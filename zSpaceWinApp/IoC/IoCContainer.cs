namespace zSpaceWinApp.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Unity;
    using Unity.Registration;
    using Unity.Resolution;

    /// <summary>
    /// Class IoCContainer.
    /// </summary>
    /// <seealso cref="LightApp.Configuration.XML.Configurations.IIoCContainer" />
    public class IoCContainer : IIoCContainer
    {
        #region Fields

        private readonly IUnityContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IoCContainer"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public IoCContainer(IUnityContainer container)
        {
            this.container = container;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Resolves the specified overrides.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="overrides">The overrides.</param>
        /// <returns>T.</returns>
        public T Resolve<T>(params IoCParameterOverride[] overrides)
        {
            return this.container.Resolve<T>(overrides.Select(o => new ParameterOverride(o.Name, o.Value)).ToArray());
        }

        /// <summary>
        /// Resolves the specified instance name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instanceName">Name of the instance.</param>
        /// <param name="overrides">The overrides.</param>
        /// <returns>T.</returns>
        public T Resolve<T>(string instanceName, params IoCParameterOverride[] overrides)
        {
            return this.container.Resolve<T>(
                instanceName,
                overrides.Select(o => new ParameterOverride(o.Name, o.Value)).ToArray());
        }

        #endregion

        #region Methods

        internal void AddInstanceOverrides(IList<KeyValuePair<Type, object>> overrides)
        {
            foreach (var instance in overrides)
            {
                KeyValuePair<Type, object> instanceClosure = instance;
                foreach (
                    ContainerRegistration registration in
                        this.container.Registrations.Where(p => p.RegisteredType == instanceClosure.Key))
                {
                    registration.LifetimeManager.RemoveValue();
                }
            }
        }

        #endregion
    }
}
