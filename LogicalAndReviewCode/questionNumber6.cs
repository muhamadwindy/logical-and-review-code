namespace LogicalAndReviewCode
{
    public class questionNumber6
    {
        public questionNumber6()
        {
            var rootNode = new TreeNode();
            while (true)
            {
                // create a new subtree of 10000 nodes
                var newNode = new TreeNode();
                for (int i = 0; i < 10000; i++)
                {
                    var childNode = new TreeNode();
                    newNode.AddChild(childNode);
                }
                rootNode.AddChild(newNode);

                // hapus subtree sebelumnya untuk mengosongkan memori
                if (rootNode.getChildrenLength() > 10)
                {
                    rootNode.RemoveFirstChild();
                }
            }
        }
    }

    internal class TreeNode
    {
        private readonly List<TreeNode> children = new List<TreeNode>();

        public void AddChild(TreeNode child)
        {
            children.Add(child);
        }

        public void RemoveFirstChild()
        {
            children.RemoveAt(0);
        }

        public int getChildrenLength()
        {
            return children.Count;
        }
    }
}