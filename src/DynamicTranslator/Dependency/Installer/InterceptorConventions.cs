﻿using System.Linq;

using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;

using DynamicTranslator.Dependency.Interceptors;
using DynamicTranslator.Orchestrators.Detector;
using DynamicTranslator.Orchestrators.Finder;

namespace DynamicTranslator.Dependency.Installer
{
    #region using

    

    #endregion

    public class InterceptorConventions : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.ComponentRegistered += KernelOnComponentRegistered;
        }

        private void KernelOnComponentRegistered(string key, IHandler handler)
        {
            var isMeanFinder = handler.ComponentModel.Implementation.GetInterfaces().Contains(typeof(IMeanFinder));
            if (isMeanFinder)
            {
                handler.ComponentModel.Interceptors.AddFirst(new InterceptorReference(typeof(ExceptionInterceptor)));
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(TextGuardInterceptor)));
            }

            var isDetector = handler.ComponentModel.Implementation.GetInterfaces().Contains(typeof(ILanguageDetector));
            if (isDetector)
            {
                handler.ComponentModel.Interceptors.AddFirst(new InterceptorReference(typeof(ExceptionInterceptor)));
            }
        }
    }
}