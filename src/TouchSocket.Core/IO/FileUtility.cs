//------------------------------------------------------------------------------
//  此代码版权（除特别声明或在XREF结尾的命名空间的代码）归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  Gitee源代码仓库：https://gitee.com/RRQM_Home
//  Github源代码仓库：https://github.com/RRQM
//  API首页：https://touchsocket.net/
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace TouchSocket.Core;

/// <summary>
/// 文件操作
/// </summary>

public static partial class FileUtility
{
    /// <summary>
    /// 删除路径文件
    /// </summary>
    /// <param name="path"></param>
    public static void Delete(string path)
    {
        if (File.Exists(path))
        {
            File.SetAttributes(path, FileAttributes.Normal);
            File.Delete(path);
        }
    }

    /// <summary>
    /// 获取不重复文件夹名称.
    /// <para>例如：NewDir已存在时，会返回NewDir(1)</para>
    /// </summary>
    /// <param name="dirName"></param>
    /// <returns></returns>
    public static string GetDuplicateDirectoryName(string dirName)
    {
        if (!Directory.Exists(dirName))
        {
            return dirName;
        }

        var index = 0;
        while (true)
        {
            index++;
            var newPath = Path.Combine(Path.GetDirectoryName(dirName), $"{Path.GetFileNameWithoutExtension(dirName)}({index})");
            if (!System.IO.Directory.Exists(newPath))
            {
                return newPath;
            }
        }
    }

    /// <summary>
    /// 获取不重复文件名。
    /// <para>例如：New.txt已存在时，会返回New(1).txt</para>
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetDuplicateFileName(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return fileName;
        }

        var index = 0;
        while (true)
        {
            index++;
            var newPath = Path.Combine(Path.GetDirectoryName(fileName), $"{Path.GetFileNameWithoutExtension(fileName)}({index}){Path.GetExtension(fileName)}");
            if (!File.Exists(newPath))
            {
                return newPath;
            }
        }
    }

    /// <summary>
    /// 获得文件Hash值
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="hash"></param>
    /// <returns></returns>
    public static string GetFileHash(string filePath, HashAlgorithm hash)
    {
        try
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var hashValue = hash.ComputeHash(fileStream);
                return BitConverter.ToString(hashValue).Replace("-", "");
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 获得文件Hash值
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns></returns>
    public static string GetFileHash256(string filePath)
    {
        try
        {
            HashAlgorithm hash = SHA256.Create();
            using (var fileStream = File.OpenRead(filePath))
            {
                var hashValue = hash.ComputeHash(fileStream);
                return BitConverter.ToString(hashValue).Replace("-", "");
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 获取文件MD5
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetFileMd5(string path)
    {
        using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            return GetStreamMd5(fileStream);
        }
    }

    /// <summary>
    /// 获取仅当前文件夹中包含的文件名称，不含全路径。
    /// </summary>
    /// <param name="dirPath"></param>
    /// <returns></returns>
    public static string[] GetIncludeFileNames(string dirPath)
    {
        return Directory.GetFiles(dirPath).Select(s => Path.GetFileName(s)).ToArray();
    }

    /// <summary>
    /// 获取相对路径。
    /// </summary>
    /// <param name="relativeTo"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetRelativePath(string relativeTo, string path)
    {
        if (string.IsNullOrEmpty(relativeTo)) throw new ArgumentNullException(nameof(relativeTo));
        if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

        var fromUri = new Uri(relativeTo);
        var toUri = new Uri(path);

        if (fromUri.Scheme != toUri.Scheme)
        {
            // 不是同一种路径，无法转换成相对路径。
            return path;
        }

        if (fromUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase)
            && !relativeTo.EndsWith("/", StringComparison.OrdinalIgnoreCase)
            && !relativeTo.EndsWith("\\", StringComparison.OrdinalIgnoreCase))
        {
            // 如果是文件系统，则视来源路径为文件夹。
            fromUri = new Uri(relativeTo + Path.DirectorySeparatorChar);
        }

        var relativeUri = fromUri.MakeRelativeUri(toUri);
        var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

        if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
        {
            relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        return relativePath;
    }

    /// <summary>
    /// 获得流Hash值
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="hash"></param>
    /// <returns></returns>
    public static string GetStreamHash(Stream stream, HashAlgorithm hash)
    {
        try
        {
            var hashValue = hash.ComputeHash(stream);
            return BitConverter.ToString(hashValue).Replace("-", "");
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 获得流Hash值
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static string GetStreamHash256(Stream stream)
    {
        try
        {
            HashAlgorithm hash = SHA256.Create();
            var hashValue = hash.ComputeHash(stream);
            return BitConverter.ToString(hashValue).Replace("-", "");
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 获取流MD5
    /// </summary>
    /// <param name="fileStream"></param>
    /// <returns></returns>
    public static string GetStreamMd5(Stream fileStream)
    {
        using (HashAlgorithm hash = System.Security.Cryptography.MD5.Create())
        {
            return GetStreamHash(fileStream, hash);
        }
    }

    /// <summary>
    /// 判断该路径是否为文件夹
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool IsDirectory(string path)
    {
        try
        {
            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                return true;
            }
        }
        catch (Exception)
        {
        }
        return false;
    }

    /// <summary>
    /// 路径格式化。
    /// </summary>
    /// <remarks>
    /// 此操作会把路径中的所有反斜杠替换为正斜杠。例如：C:\\a.txt替换为C:/a.txt
    /// </remarks>
    /// <param name="pathString"></param>
    /// <returns></returns>
    public static string PathFormat(string pathString)
    {
        return pathString.Replace('\\', Path.AltDirectorySeparatorChar);
        //return pathString.Replace('\\', Path.PathSeparator);
    }

    /// <summary>
    /// 转化为文件大小的字符串，类似10B，10Kb，10Mb，10Gb。
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string ToFileLengthString(long length)
    {
        const decimal kb = 1024;
        const decimal mb = 1024 * kb;
        const decimal gb = 1024 * mb;
        const decimal tb = 1024 * gb;
        const decimal pb = 1024 * tb;

        if (length < kb)
        {
            return $"{length}B";
        }
        if (length < mb)
        {
            return $"{(length / kb):F2}Kb";
        }
        if (length < gb)
        {
            return $"{(length / mb):F2}Mb";
        }
        if (length < tb)
        {
            return $"{(length / gb):F2}Gb";
        }
        if (length < pb)
        {
            return $"{(length / tb):F2}Tb";
        }
        return $"{(length / pb):F2}Pb";
    }
}