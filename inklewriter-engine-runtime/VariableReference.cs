﻿using Newtonsoft.Json;

namespace Inklewriter.Runtime
{
    internal class VariableReference : Runtime.Object
    {
        // Normal named variable
        [JsonProperty("get")]
        [UniqueJsonIdentifier]
        public string name { get; set; }

        // Variable reference is actually a path for a visit (read) count
        public Path pathForCount { get; set; }

        // beat count or read count?
        [JsonProperty("beat")]
        public bool isBeatCount { get; set; }

        [JsonProperty("readCount")]
        [UniqueJsonIdentifier]
        public string pathStringForCount { 
            get {
                if( pathForCount == null )
                    return null;

                return pathForCount.componentsString;
            }
            set {
                if (value == null)
                    pathForCount = null;
                else
                    pathForCount = new Path (value);
            }
        }

        public VariableReference (string name)
        {
            this.name = name;
        }

        // Require default constructor for serialisation
        public VariableReference() {}

        public override string ToString ()
        {
            if (name != null) {
                return string.Format ("var({0})", name);
            } else {
                var pathStr = pathStringForCount;
                if( isBeatCount ) {
                    return string.Format("beats_since({0})", pathStr);
                } else {
                    return string.Format("read_count({0})", pathStr);
                }
            }
        }
    }
}

