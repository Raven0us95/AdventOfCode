namespace AdventOfCode2023.Helper
{
    public class Node
    {
        public Node(char symbol, int positionX, int positionY)
        {
            Symbol = symbol;
            Position = new Position(positionX, positionY);
        }
        public string MoveDirection = string.Empty;
        public Position Position {  get; private set; }
        public char Symbol;
        public Node PreviousNode { get; set; }
        public Node NextNode { get; set; }
    }
}