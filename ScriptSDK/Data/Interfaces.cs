namespace ScriptSDK.Data
{
    /// <summary>
    /// Interface storing 2 dimensional point.
    /// </summary>
    public interface IPoint2D
    {
        /// <summary>
        /// Stores x-axis.
        /// </summary>
        int X { get; }
        /// <summary>
        /// Stores y-axis. 
        /// </summary>
        int Y { get; }
    }

    /// <summary>
    /// Interface storing 3 dimensional point.
    /// </summary>
    public interface IPoint3D : IPoint2D
    {
        /// <summary>
        /// Stores z-axis.
        /// </summary>
        int Z { get; }
    }


    /// <summary>
    /// Interface describes the root level shared data of gump controls and components.<br/>
    /// <b>Should never used by SDK-User, since its a designer interface for mapping data internal!</b>
    /// </summary>
    public interface IGumpElement
    {
        /// <summary>
        /// Stores ElementID which expose the unique queue number of element.
        /// </summary>
        int ElementID { get; }
        /// <summary>
        /// Stores PageID which expose on which layer of gump the element is assigned.
        /// </summary>
        int Page { get; }
    }

    /// <summary>
    /// Interface describes the common shared data of any gump control.
    /// </summary>
    public interface IGumpControl : IGumpElement
    {
        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        Point2D Location { get; }
    }

    /// <summary>
    /// Interface describes the common shared data of any gump component.
    /// </summary>
    public interface IGumpComponent : IGumpElement
    { }
}