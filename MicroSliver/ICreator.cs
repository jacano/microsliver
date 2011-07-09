// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
namespace MicroSliver
{
    /// <summary>
    /// Provides a class to control object instantiation.
    /// </summary>
    public interface ICreator
    {
        /// <summary>
        /// Instantiates an concrete implementation for the interface that holds this mapping.
        /// </summary>
        object Create();
    }
}
