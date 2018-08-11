using System;

namespace ChatBot
{
    enum Strategy
    {
		/// <summary>
		/// random choice of the answer
		/// </summary>
		Rand,

		/// <summary>
		/// choose from top to bottom
		/// </summary>
		UpSeq,

		/// <summary>
		/// choose from bottom to top
		/// </summary>
		DownSeq,

		// TODO : extend
	}
}
