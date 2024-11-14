using UnityPlugins.Common.Logic;

namespace UnityPlugins.Input.Unity
{
	public enum EInputStatus : byte
	{
		Released = 0,
		JustReleased = 1,
		JustPressed = 2,
		Pressed = 3,
	}

	public static class EInputStatusExt
	{
		public static readonly EnumExt<EInputStatus> Meta = new EnumExt<EInputStatus>();

		public static bool IsPressed(this EInputStatus status)
		{
			return status == EInputStatus.Pressed || status == EInputStatus.JustPressed;
		}

		public static bool IsReleased(this EInputStatus status)
		{
			return status == EInputStatus.Released || status == EInputStatus.JustReleased;
		}

		#region GetNext
		private static readonly EInputStatus[] m_GetNextActive = new EInputStatus[]
		{
			 EInputStatus.JustPressed,
			 EInputStatus.JustPressed,
			 EInputStatus.Pressed,
			 EInputStatus.Pressed,
		};

		private static readonly EInputStatus[] m_GetNextInActive = new EInputStatus[]
		{
			 EInputStatus.Released,
			 EInputStatus.Released,
			 EInputStatus.JustReleased,
			 EInputStatus.JustReleased,
		};

		public static EInputStatus GetNext(this EInputStatus current, bool isActive)
		{
			if(isActive)
			{
				return m_GetNextActive[(int)current];
			}
			else
			{
				return m_GetNextInActive[(int)current];
			}
		}
		#endregion
	}
}
