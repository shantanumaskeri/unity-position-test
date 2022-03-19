using JetBrains.Annotations;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public abstract class SymbolPresenter
    {
        protected SymbolPresenter(Image image)
        {
        }
    }

    [UsedImplicitly]
    public sealed class PlayerSymbolPresenter : SymbolPresenter
    {
        public PlayerSymbolPresenter(Image image) : base(image)
        {
        }
    }

    [UsedImplicitly]
    public sealed class OpponentSymbolPresenter : SymbolPresenter
    {
        public OpponentSymbolPresenter(Image image) : base(image)
        {
        }
    }
}