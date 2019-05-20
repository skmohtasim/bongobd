using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeastCommonAncestor
{
    public class Node
    {
        public int data;
        public Node left, right;

        public Node(int item)
        {
            data = item;
            left = right = null;
        }
    }

    public class Program
    {
        public Node root;

        /* Function to find LCA of n1 and n2. The function assumes that both  
           n1 and n2 are present in BST */
        public virtual Node lca(Node node, int n1, int n2)
        {
            if (node == null)
            {
                return null;
            }

            // If both n1 and n2 are smaller than root, then LCA lies in left  
            if (node.data > n1 && node.data > n2)
            {
                return lca(node.left, n1, n2);
            }

            // If both n1 and n2 are greater than root, then LCA lies in right  
            if (node.data < n1 && node.data < n2)
            {
                return lca(node.right, n1, n2);
            }

            return node;
        }

        /* Driver program to test lca() */
        public static void Main(string[] args)
        {
            // Let us construct the BST shown in the above figure  
            Program tree = new Program();
            tree.root = new Node(1);
            tree.root.left = new Node(2);
            tree.root.right = new Node(3);
            tree.root.left.left = new Node(4);
            tree.root.left.right = new Node(5);
            tree.root.right.left= new Node(6);
            tree.root.right.left = new Node(7);
            tree.root.left.left.left = new Node(8);
            tree.root.left.left.right = new Node(9);

            int n1 = 3, n2 = 7;
            Node t = tree.lca(tree.root, n1, n2);
            Console.WriteLine("LCA of " + n1 + " and " + n2 + " is " + t.data);

            n1 = 3; n2 = 6;
            t = tree.lca(tree.root, n1, n2);
            Console.WriteLine("LCA of " + n1 + " and " + n2 + " is " + t.data);
            Console.ReadKey();
        }
    }
}
