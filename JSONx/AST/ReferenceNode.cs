namespace JSONx.AST
{
    public class ReferenceNode : JSONxNode
    {
        public string FilePath { get; set; }
        public string ObjectPath { get; set; }
        public override bool HasChildren => false;

        public ReferenceNode(string file, string path) : base(NodeType.Reference)
        {
            FilePath = file;
            ObjectPath = path;
        }
    }
}