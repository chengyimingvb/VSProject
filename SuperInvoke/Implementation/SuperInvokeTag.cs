using System;
using System.Collections.Generic;

namespace Invoke
{

    internal sealed class SuperInvokeTag : IEquatable<SuperInvokeTag> {

        private static long stGenCount;
        private const char ST_GEN_PREFIX = 'Σ';

        private static long taskIds = long.MinValue;
        private static readonly Dictionary<string, SuperInvokeTag> NamedTags = new Dictionary<string, SuperInvokeTag>(); 

        private readonly long id;

        internal static SuperInvokeTag CreateInstance() {
            return new SuperInvokeTag();
        }

        internal static SuperInvokeTag GetInstance(string name) {
            SuperInvokeTag tagWithName = null;

            if (name != null) {
                if (NamedTags.ContainsKey(name)) {
                    tagWithName = NamedTags[name];
                }
                else {
                    tagWithName = new SuperInvokeTag();
                    NamedTags.Add(name, tagWithName);
                }
            }
            
            return tagWithName;
        }

        internal static string CreateStringTag() {
            string newTag = ST_GEN_PREFIX + (stGenCount++).ToString();
            GetInstance(newTag);

            return newTag;
        }


        private SuperInvokeTag() {
            id = taskIds++;
        }
        
        public bool Equals(SuperInvokeTag other) {
            return other != null
                && id == other.id;
        }

        public override bool Equals(object other) {
            return other is SuperInvokeTag
                && Equals((SuperInvokeTag)other);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }

}