using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Utility.Services
{
    public sealed class LoadingService
    {
        public async UniTask StartLoading(ILoadUnit loadUnit, bool throwException = true)
        {
            try
            {
                await loadUnit.Load();
            }
            catch (Exception ex)
            {
                if (throwException)
                    throw ex;
            }
            finally
            {
                await OnLoadingIsOver();
            }
        }

        public async UniTask StartLoading<T>(ILoadUnit<T> loadUnit, T value, bool throwException = true)
        {
            try
            {
                await loadUnit.Load(value);
            }
            catch (Exception ex)
            {
                if(throwException)
                    throw ex;
            }
            finally
            {
                await OnLoadingIsOver();
            }
        }

        public async UniTask StartLoadingInParallel(bool throwException = true, params ILoadUnit[] loadUnits)
        {
            try
            {
                //maybe it's better to cache lambda expression | has to be profiled firstly
                var task = UniTask.WhenAll(loadUnits.Select(loadUnit => loadUnit.Load()));
                await task;
            }
            catch (Exception ex)
            {
                if(throwException)
                    throw ex;
            }
            finally 
            { 
                await OnLoadingIsOver(); 
            }
        }
        
        private async UniTask OnLoadingIsOver()
        {
            int currentThreadID = Thread.CurrentThread.ManagedThreadId;
            int mainThreadID = PlayerLoopHelper.MainThreadId;

            if (mainThreadID == currentThreadID)
                return;

            await UniTask.SwitchToMainThread();
        }
    }

    public interface ILoadUnit
    {
        UniTask Load();
    }

    public interface ILoadUnit<in T>
    {
        UniTask Load(T value);
    }
}