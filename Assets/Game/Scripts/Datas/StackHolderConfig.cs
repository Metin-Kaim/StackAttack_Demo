using TMPro;

namespace Assets.Game.Scripts.Datas
{
    [System.Serializable]
    public struct StackHolderConfig
    {
        public byte StackSize;
        public byte SizeMultiplier;
        public TextMeshPro StackText;
        public ColorType ColorType;
    }
}