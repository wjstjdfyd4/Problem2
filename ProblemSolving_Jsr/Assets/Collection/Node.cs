namespace Mc
{
    // ���׸� ��� Ŭ����
    public class Node<H>
    {
        internal H data; // ����� ������
        internal Node<H> next; // ���� ��带 ����Ű�� ��ũ

        // ����� ������
        public Node(H data)
        {
            this.data = data; // �־��� �����ͷ� �ʱ�ȭ
            next = null; // ���� ���� �ʱ⿡�� ����
        }
    }
}
