using UnityEngine;

namespace Mc
{
    public class Queue<A> : MonoBehaviour
    {
        private LinkedList<A> list = new LinkedList<A>();

        // 큐에 요소를 추가하는 메서드
        public void Enqueue(A data)
        {
            list.Insert(data);
        }

        // 큐에서 요소를 제거하고 반환하는 메서드
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
