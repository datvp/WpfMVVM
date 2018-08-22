namespace zSpaceWinApp.IoC
{
    /// <summary>
    /// Class IoCContainerFactory.
    /// </summary>
    public static class IoCContainerFactory
    {
        #region Public Properties

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public static IIoCContainer Current { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Initializes the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public static void Initialize(IIoCContainer container)
        {
            Current = container;
        }

        #endregion
    }

    /// <summary>
    /// Interface IIoCContainer
    /// </summary>
    public interface IIoCContainer
    {
        #region Public Methods and Operators

        /// <summary>
        /// Resolves the specified overrides.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="overrides">The overrides.</param>
        /// <returns>T.</returns>
        T Resolve<T>(params IoCParameterOverride[] overrides);

        /// <summary>
        /// Resolves the specified instance name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instanceName">Name of the instance.</param>
        /// <param name="overrides">The overrides.</param>
        /// <returns>T.</returns>
        T Resolve<T>(string instanceName, params IoCParameterOverride[] overrides);

        #endregion
    }

    /// <summary>
    /// Class IoCParameterOverride.
    /// </summary>
    public class IoCParameterOverride
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IoCParameterOverride"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public IoCParameterOverride(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; private set; }

        #endregion
    }
}
