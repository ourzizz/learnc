class TreeNode(object):
    def __init__(self, data=0,left=0,right=0):
        self.data=data
        self.left=left
        self.right =right

class BTree(object):
    def __init__(self, root=0):
        self.root=root
    def is_empty(self):
        if self.root is 0:
            return True
        else:
            return False

    def SearchBst(self,root,key,father):
        p=root
        father.left=0
        father.right=0
        while p!=0 and p.data!=key:
            father=p
            if key>p.data:
                p=p.right
            else:
                p=p.left
        return p,father

    def InertBST(self,root,e):
        father=TreeNode(0)
        p=self.SearchBst(root,e,father)
        if root is 0:
            root=TreeNode(e)
        if p is 0:
            p=TreeNode(e,0,0)
            print p.data
            print father.data
            if e>father.data:
                father.right=p
            else:
                father.left=p

    def inorder(self, treenode):
        if treenode is 0:
            return 
        self.inorder(treenode.left)
        print treenode.data,
        self.inorder(treenode.right)
if __name__ == "__main__":
    n1 = TreeNode(data=2)
    n2 = TreeNode(4,n1,0)
    n3 = TreeNode(6)
    n4 = TreeNode(8)
    n5 = TreeNode(7,n3,n4)
    n6 = TreeNode(5,n2,n5)
    n7 = TreeNode(9,n6,0)
    n8 = TreeNode(20)
    root = TreeNode(10,n7,n8)
    bt=BTree(root)
    father=TreeNode(0)
    print bt.inorder(bt.root)
    print bt.SearchBst(root,8,father)

    bt.InertBST(bt.root,40)
    print bt.inorder(bt.root)
