// Guids.cs
// MUST match guids.h
using System;

namespace ArtiomK.Romania
{
    static class GuidList
    {
        public const string guidRomaniaPkgString = "b45f76ac-6feb-4f66-8714-124de9918368";
        public const string guidRomaniaCmdSetString = "b263bb83-1275-4ec2-844a-8993f064ad62";

        public static readonly Guid guidRomaniaCmdSet = new Guid(guidRomaniaCmdSetString);
    };
}