using UnityEngine;

namespace Mc
{
    public class Queue<A> : MonoBehaviour
    {
        private LinkedList<A> list = new LinkedList<A>();

        // ť�� ��Ҹ� �߰��ϴ� �޼���
        public void Enqueue(A data)
        {
            list.Insert(data);
        }

        // ť���� ��Ҹ� �����ϰ� ��ȯ�ϴ� �޼���
        public A Dequeue()
        {
            if (list.head != null)
            {
                A returnData = list.head.data;
                list.Delete(list.head.data);
                return returnData;
            }
            else
            {
                throw new System.InvalidOperationException("Queue is empty");
            }
        }
    }
}
