namespace Mc
{
    // 제네릭 노드 클래스
    public class Node<H>
    {
        internal H data; // 노드의 데이터
        internal Node<H> next; // 다음 노드를 가리키는 링크

        // 노드의 생성자
        public Node(H data)
        {
            this.data = data; // 주어진 데이터로 초기화
            next = null; // 다음 노드는 초기에는 없음
        }
    }
}