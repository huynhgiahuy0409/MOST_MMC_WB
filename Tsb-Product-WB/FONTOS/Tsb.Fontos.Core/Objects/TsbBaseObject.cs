using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using Tsb.Fontos.Core.Xml;

namespace Tsb.Fontos.Core.Objects
{
    /// <summary>
    /// TSB Base Object class
    /// </summary>
    [Serializable]
    public class TsbBaseObject : MarshalByRefObject, ITsbBaseObject, ICloneable
    {
        #region FIELD AREA *************************************
        private string _objectID = null;
        private ObjectType _objectType = default(ObjectType);
        #endregion

        #region PROPERTY AREA **********************************
        /// <summary>
        /// Gets and Sets Object ID
        /// </summary>
        [XmlIgnore]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectID
        {
            get { return _objectID; }
            set { _objectID = value; }
        }

        /// <summary>
        /// Gets and Sets Object Type
        /// </summary>
        [XmlIgnore]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObjectType ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TsbBaseObject()
        {
            this.ObjectID = "GNR-FTCO-OBJ-TsbBaseObject";
            this.ObjectType = ObjectType.DEFAULT;
        }
        #endregion

        #region INSTNACE METHOD AREA ***************************
        /// <summary>
        /// Clone the object, and returning a reference to a cloned object.
        /// </summary>
        /// <returns>Reference to the new cloned object.</returns>
        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Deep clone the object, and returning a reference to a cloned object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Reference to the new cloned object.</returns>
        protected virtual T DeepClone<T>()
        {
            T item = default(T);

            try
            {
                string xmlData = XmlUtil.SerializeToXmlString<T>(this);
                item = XmlUtil.DeserializeFromString<T>(xmlData);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }
        /// <summary>
        /// Clone the list, and returning a reference to a cloned list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        protected virtual List<T> CloneList<T>(List<T> sourceList)
        {
            if (sourceList == null)
            {
                return null;
            }

            List<T> newList = new List<T>();

            try
            {
                foreach (ICloneable item in sourceList)
                {
                    if (item != null)
                    {
                        newList.Add((T)item.Clone());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newList;
        }
        #endregion

    }
}
