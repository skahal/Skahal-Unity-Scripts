using System;

namespace Skahal.Infrastructure.Framework.Domain
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    [Serializable]
    public abstract class EntityBase : IEntity
    {
        #region Properties		
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is a new one.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </value>
        public bool IsNew
        {
            get
            {
                return Id == 0;
            }
        }
        #endregion
    }
}
