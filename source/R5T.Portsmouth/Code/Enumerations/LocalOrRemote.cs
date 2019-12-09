using System;


namespace R5T.Portsmouth
{
    public enum LocalOrRemote
    {
        Invalid = 0, // Avoid mis-configuration errors by making the default unconfigured value invalid.
        Local,
        Remote,
    }
}
