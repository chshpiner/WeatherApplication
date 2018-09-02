#include "stdafx.h"
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTest1
{		

	TEST_CLASS(UnitTest1)
	{
	public:
		[DeploymentItem(@"x86\SQLite.Interop.dll", "x86")] // this is the key
		TEST_METHOD(TestMethod1)
		{
			// TODO: Your test code here
		}

	};
}
