//using System;
//using System.Collections.Generic;
//using UnityEngine;
//
//namespace GameGraph
//{
//    [Serializable]
//    public class Node
//    {
//        public string name;
//        public Vector2 position;
//        public Dictionary<string, ValueEntry> data = new Dictionary<string, ValueEntry>();
//
//        public Node()
//        {
//        }
//
//        public Node(string name)
//        {
//            this.name = name;
//        }
//
//        public Node(string name, Vector2 position)
//        {
//            this.name = name;
//            this.position = position;
//        }
//
//        public Node(string name, Vector2 position, Dictionary<string, ValueEntry> data)
//        {
//            this.name = name;
//            this.position = position;
//            this.data = data;
//        }
//    }
//
//    [Serializable]
//    public struct ValueEntry
//    {
//        // TODO Value does not belong in the serialized data, but accessed via reflection maybe?
//        [Obsolete]
//        public object value;
//        public Node ingoingLink;
//        public Node outgoingLink;
//    }
//}
