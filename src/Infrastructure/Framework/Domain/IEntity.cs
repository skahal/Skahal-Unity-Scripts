namespace Skahal.Infrastructure.Framework.Domain
{
    /// <summary>
    /// Defines an interface to a basic entity.
    /// </summary>
    public interface IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is a new one.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </value>
        bool IsNew { get; }
        #endregion
    }
}
