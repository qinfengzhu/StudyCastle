namespace NVelocity.Test
{
	using System;
	using System.Collections;
	using System.IO;
	using System.Text;
	using NUnit.Framework;
	using NVelocity.Runtime;
	using NVelocity.Util;

	/// <summary>
	/// This is a base interface that contains a bunch of static final
	/// strings that are of use when testing templates.
	/// </summary>
	/// <author> <a href="mailto:jon@latchkey.com">Jon S. Stevens</a></author>
	public struct TemplateTestBase_Fields
	{
		/// <summary>
		/// VTL file extension.
		/// </summary>
		public const String TMPL_FILE_EXT = "vm";
		/// <summary>
		/// Comparison file extension.
		/// </summary>
		public const String CMP_FILE_EXT = "cmp";
		/// <summary>
		/// Comparison file extension.
		/// </summary>
		public const String RESULT_FILE_EXT = "res";
		/// <summary>
		/// Path for templates. This property will override the
		/// value in the default velocity properties file.
		/// </summary>
		public const String FILE_RESOURCE_LOADER_PATH = "../../test/templates";

		/// <summary>
		/// Properties file that lists which template tests to run.
		/// </summary>
		public static String TEST_CASE_PROPERTIES;

		/// <summary>
		/// Results relative to the build directory.
		/// </summary>
		public static String RESULT_DIR;

		/// <summary>
		/// Results relative to the build directory.
		/// </summary>
		public static String COMPARE_DIR;

		static TemplateTestBase_Fields()
		{
			TEST_CASE_PROPERTIES = TemplateTestBase_Fields.FILE_RESOURCE_LOADER_PATH + "/templates.properties";
			RESULT_DIR = TemplateTestBase_Fields.FILE_RESOURCE_LOADER_PATH + "/results";
			COMPARE_DIR = TemplateTestBase_Fields.FILE_RESOURCE_LOADER_PATH + "/compare";
		}
	}


	/// <summary>
	/// Base test case that provides a few utility methods for
	/// the rest of the tests.
	/// </summary>
	/// <author> <a href="mailto:dlr@finemaltcoding.com">Daniel Rall</a></author>
	public class BaseTestCase
	{
		/// <summary>
		/// Concatenates the file name parts together appropriately.
		/// </summary>
		/// <returns>
		/// The full path to the file.
		/// </returns>
		protected internal static String getFileName(String dir, String base_Renamed, String ext)
		{
			StringBuilder buf = new StringBuilder();
			if (dir != null)
			{
				buf.Append(dir).Append('/');
			}
			buf.Append(base_Renamed).Append('.').Append(ext);
			return buf.ToString();
		}

		/// <summary>
		/// Assures that the results directory exists.  If the results directory
		/// cannot be created, fails the test.
		/// </summary>
		protected internal static void assureResultsDirectoryExists(String resultsDirectory)
		{
			FileInfo dir = new FileInfo(resultsDirectory);
			bool tmpBool;
			if (File.Exists(dir.FullName))
				tmpBool = true;
			else
				tmpBool = Directory.Exists(dir.FullName);
			if (!tmpBool)
			{
				RuntimeSingleton.info("Template results directory does not exist");
				Boolean ok = true;
				try
				{
					Directory.CreateDirectory(dir.FullName);
				}
				catch (Exception ex)
				{
					ok = false;
				}

				if (ok)
				{
					RuntimeSingleton.info("Created template results directory");
				}
				else
				{
					String errMsg = "Unable to create template results directory";
					RuntimeSingleton.warn(errMsg);
					Assertion.Fail(errMsg);
				}
			}
		}


		/// <summary>
		/// Normalizes lines to account for platform differences.  Macs use
		/// a single \r, DOS derived operating systems use \r\n, and Unix
		/// uses \n.  Replace each with a single \n.
		/// </summary>
		/// <author> <a href="mailto:rubys@us.ibm.com">Sam Ruby</a>
		/// </author>
		/// <returns>
		/// source with all line terminations changed to Unix style
		/// </returns>
		protected internal virtual String normalizeNewlines(String source)
		{
			//TODO:
			//return perl.substitute("s/\r[\n]/\n/g", source);
			return source.Replace(Environment.NewLine, "|").Replace("\n", "|");
		}

		/// <summary>
		/// Returns whether the processed template matches the
		/// content of the provided comparison file.
		/// </summary>
		/// <returns>
		/// Whether the output matches the contents
		/// of the comparison file.
		/// </returns>
		/// <exception cref="">
		/// Exception Test failure condition.
		/// </exception>
		protected internal virtual bool isMatch(String resultsDir, String compareDir, String baseFileName, String resultExt, String compareExt)
		{
			Boolean SHOW_RESULTS = true;

			String result = StringUtils.fileContentsToString(getFileName(resultsDir, baseFileName, resultExt));
			String compare = StringUtils.fileContentsToString(getFileName(compareDir, baseFileName, compareExt));

			String s1 = normalizeNewlines(result);
			String s2 = normalizeNewlines(compare);

			Boolean equals = s1.Equals(s2);
			if (!equals && SHOW_RESULTS)
			{
				String[] cmp = compare.Split(Environment.NewLine.ToCharArray());
				String[] res = result.Split(Environment.NewLine.ToCharArray());

				IEnumerator cmpi = cmp.GetEnumerator();
				IEnumerator resi = res.GetEnumerator();
				Int32 line = 0;
				while (cmpi.MoveNext() && resi.MoveNext())
				{
					line++;
					if (!cmpi.Current.Equals(resi.Current))
					{
						Console.Out.WriteLine(line.ToString() + " : " + cmpi.Current.ToString());
						Console.Out.WriteLine(line.ToString() + " : " + resi.Current.ToString());
					}
				}
			}

			return equals;
		}

		/// <summary>
		/// Turns a base file name into a test case name.
		/// </summary>
		/// <param name="s">
		/// The base file name.
		/// </param>
		/// <returns>
		/// The test case name.
		/// </returns>
		protected internal static String getTestCaseName(String s)
		{
			StringBuilder name = new StringBuilder();
			name.Append(Char.ToUpper(s[0]));
			name.Append(s.Substring(1, (s.Length) - (1)).ToLower());
			return name.ToString();
		}
	}
}