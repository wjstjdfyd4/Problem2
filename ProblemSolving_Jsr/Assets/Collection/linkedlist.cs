using UnityEngine;

namespace Mc
{
    public class LinkedList<H>
    {
        public Node<H> head;

        // 리스트에 노드를 추가하는 메서드
        public void Insert(H data)
        {
            Node<H> node = new Node<H>(data);
            if (head == null)
                head = node;
            else
            {
                Node<H> temp = head;
                while (temp.next != null)
                    temp = temp.next;
                temp.next = node;
            }
        }

        // 리스트에서 노드를 삭제하는 메서드
        public void Delete(H key)
        {
            if (head == null)
                return;
            if (head.data.Equals(key))
            {
                head = head.next;
                return;
            }
            Node<H> temp = head;
            while (temp.next != null && !temp.next.data.Equals(key))
                temp = temp.next;
            if (temp.next != null)
                temp.next = temp.next.next;
        }

        // 리스트를 역순으로 바꾸는 메서드
        public void Reverse()
        {
            Node<H> prev = null;
            Node<H> current = head;
            while (current != null)
            {
                Node<H> nextTemp = current.next;
                current.next = prev;
                prev = current;
                current = nextTemp;
            }
            head = prev;
        }

        // 리스트를 출력하는 메서드
        public void Print()
        {
            for (Node<H> temp = head; temp != null; temp = temp.next)
                Debug.Log(temp.data);
        }
    }
}
