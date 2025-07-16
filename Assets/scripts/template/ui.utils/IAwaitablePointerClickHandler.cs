using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;

namespace utils
{
    public interface IAwaitablePointerClickHandler : IPointerClickHandler
    {
        UniTask onClickAwaitable();
    }
}