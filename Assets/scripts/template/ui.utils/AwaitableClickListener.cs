using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace utils
{
    public class AwaitableClickListener<T>
    {
        private UniTaskCompletionSource<T> _completionSource;

        public void notifyClick(T item)
        {
            _completionSource?.TrySetResult(item);
        }
        
        public async UniTask<T> awaitClick()
        {
            _completionSource?.TrySetCanceled();
            _completionSource = new();
            return await _completionSource.Task;
        } 
        
        public async UniTask<T> awaitClickWithCancellation(CancellationToken token)
        {
            _completionSource?.TrySetCanceled();
            _completionSource = new();
            return await _completionSource.Task.AttachExternalCancellation(token);
        } 
    }
}