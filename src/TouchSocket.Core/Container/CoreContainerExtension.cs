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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using TouchSocket.Resources;

namespace TouchSocket.Core;

/// <summary>
/// ContainerExtension
/// </summary>
public static class CoreContainerExtension
{
    /// <summary>
    /// DynamicallyAccessed
    /// </summary>
    public const DynamicallyAccessedMemberTypes DynamicallyAccessed = DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.PublicProperties;

    #region RegisterSingleton

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom, [DynamicallyAccessedMembers(DynamicallyAccessed)] TTo>(this IRegistrator registrator, TTo instance)
                 where TFrom : class
          where TTo : class, TFrom
    {
        RegisterSingleton(registrator, typeof(TFrom), instance);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton(this IRegistrator registrator, object instance)
    {
        if (instance is null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        RegisterSingleton(registrator, instance.GetType(), instance);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IRegistrator RegisterSingleton(this IRegistrator registrator, [DynamicallyAccessedMembers(DynamicallyAccessed)] Type fromType)
    {
        if (fromType is null)
        {
            throw new ArgumentNullException(nameof(fromType));
        }

        RegisterSingleton(registrator, fromType, fromType);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="key"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom, [DynamicallyAccessedMembers(DynamicallyAccessed)] TTo>(this IRegistrator registrator, string key, TTo instance)
                          where TFrom : class
                  where TTo : class, TFrom
    {
        RegisterSingleton(registrator, typeof(TFrom), instance, key);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="instance"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton(this IRegistrator registrator, Type fromType, object instance, string key)
    {
        registrator.Register(new DependencyDescriptor(fromType, instance), key);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton(this IRegistrator registrator, Type fromType, object instance)
    {
        registrator.Register(new DependencyDescriptor(fromType, instance));
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="instance"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom>(this IRegistrator registrator, object instance, string key)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), instance), key);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom>(this IRegistrator registrator, object instance)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), instance));
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="instance"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton(this IRegistrator registrator, object instance, string key)
    {
        registrator.Register(new DependencyDescriptor(instance.GetType(), instance), key);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <param name="registrator"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<[DynamicallyAccessedMembers(DynamicallyAccessed)] TFrom>(this IRegistrator registrator)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), typeof(TFrom), Lifetime.Singleton));
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<[DynamicallyAccessedMembers(DynamicallyAccessed)] TFrom>(this IRegistrator registrator, string key)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), typeof(TFrom), Lifetime.Singleton), key);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="toType"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton(this IRegistrator registrator, Type fromType, [DynamicallyAccessedMembers(DynamicallyAccessed)] Type toType)
    {
        registrator.Register(new DependencyDescriptor(fromType, toType, Lifetime.Singleton));
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="toType"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton(this IRegistrator registrator, Type fromType, [DynamicallyAccessedMembers(DynamicallyAccessed)] Type toType, string key)

    {
        registrator.Register(new DependencyDescriptor(fromType, toType, Lifetime.Singleton), key);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom>(this IRegistrator registrator, Func<IResolver, object> func)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), Lifetime.Singleton)
        {
            ImplementationFactory = func
        });
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom, [DynamicallyAccessedMembers(DynamicallyAccessed)] TTo>(this IRegistrator registrator, Func<IResolver, object> func)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), typeof(TTo), Lifetime.Singleton)
        {
            ImplementationFactory = func
        });
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="func"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom>(this IRegistrator registrator, Func<IResolver, object> func, string key)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), Lifetime.Singleton)
        {
            ImplementationFactory = func
        }, key);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="func"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom, [DynamicallyAccessedMembers(DynamicallyAccessed)] TTo>(this IRegistrator registrator, Func<IResolver, object> func, string key)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), typeof(TTo), Lifetime.Singleton)
        {
            ImplementationFactory = func
        }, key);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton(this IRegistrator registrator, Type fromType, Func<IResolver, object> func)
    {
        registrator.Register(new DependencyDescriptor(fromType, Lifetime.Singleton)
        {
            ImplementationFactory = func
        });
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="func"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton(this IRegistrator registrator, Type fromType, Func<IResolver, object> func, string key)
    {
        registrator.Register(new DependencyDescriptor(fromType, Lifetime.Singleton)
        {
            ImplementationFactory = func
        }, key);
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTO"></typeparam>
    /// <param name="registrator"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom, [DynamicallyAccessedMembers(DynamicallyAccessed)] TTO>(this IRegistrator registrator) where TFrom : class
                 where TTO : class, TFrom
    {
        RegisterSingleton(registrator, typeof(TFrom), typeof(TTO));
        return registrator;
    }

    /// <summary>
    /// 注册单例
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTO"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterSingleton<TFrom, [DynamicallyAccessedMembers(DynamicallyAccessed)] TTO>(this IRegistrator registrator, string key)
                  where TFrom : class
          where TTO : class, TFrom
    {
        RegisterSingleton(registrator, typeof(TFrom), typeof(TTO), key);
        return registrator;
    }

    #endregion RegisterSingleton

    #region Transient

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTO"></typeparam>
    /// <param name="registrator"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient<TFrom, [DynamicallyAccessedMembers(DynamicallyAccessed)] TTO>(this IRegistrator registrator)
                where TFrom : class
         where TTO : class, TFrom
    {
        RegisterTransient(registrator, typeof(TFrom), typeof(TTO));
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <param name="registrator"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient<[DynamicallyAccessedMembers(DynamicallyAccessed)] TFrom>(this IRegistrator registrator)
                  where TFrom : class
    {
        RegisterTransient(registrator, typeof(TFrom), typeof(TFrom));
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient<[DynamicallyAccessedMembers(DynamicallyAccessed)] TFrom>(this IRegistrator registrator, string key)
                  where TFrom : class
    {
        RegisterTransient(registrator, typeof(TFrom), typeof(TFrom), key);
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTO"></typeparam>
    /// <param name="registrator"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient<TFrom, [DynamicallyAccessedMembers(DynamicallyAccessed)] TTO>(this IRegistrator registrator, string key)
                 where TFrom : class
         where TTO : class, TFrom
    {
        RegisterTransient(registrator, typeof(TFrom), typeof(TTO), key);
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient(this IRegistrator registrator, [DynamicallyAccessedMembers(DynamicallyAccessed)] Type fromType)
    {
        RegisterTransient(registrator, fromType, fromType);
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient(this IRegistrator registrator, [DynamicallyAccessedMembers(DynamicallyAccessed)] Type fromType, string key)
    {
        RegisterTransient(registrator, fromType, fromType, key);
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="toType"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient(this IRegistrator registrator, Type fromType, [DynamicallyAccessedMembers(DynamicallyAccessed)] Type toType)
    {
        registrator.Register(new DependencyDescriptor(fromType, toType, Lifetime.Transient));
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="toType"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient(this IRegistrator registrator, Type fromType, [DynamicallyAccessedMembers(DynamicallyAccessed)] Type toType, string key)
    {
        registrator.Register(new DependencyDescriptor(fromType, toType, Lifetime.Transient), key);
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient<TFrom>(this IRegistrator registrator, Func<IResolver, object> func)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), Lifetime.Transient)
        {
            ImplementationFactory = func
        });
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="func"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient<TFrom>(this IRegistrator registrator, Func<IResolver, object> func, string key)
    {
        registrator.Register(new DependencyDescriptor(typeof(TFrom), Lifetime.Transient)
        {
            ImplementationFactory = func
        }, key);
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient(this IRegistrator registrator, Type fromType, Func<IResolver, object> func)
    {
        registrator.Register(new DependencyDescriptor(fromType, Lifetime.Transient)
        {
            ImplementationFactory = func
        });
        return registrator;
    }

    /// <summary>
    /// 注册临时映射
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="func"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator RegisterTransient(this IRegistrator registrator, Type fromType, Func<IResolver, object> func, string key)
    {
        registrator.Register(new DependencyDescriptor(fromType, Lifetime.Transient)
        {
            ImplementationFactory = func
        }, key);
        return registrator;
    }

    #endregion Transient

    #region Unregister

    /// <summary>
    /// 移除注册信息
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <returns></returns>
    public static IRegistrator Unregister(this IRegistrator registrator, Type fromType)
    {
        registrator.Unregister(new DependencyDescriptor(fromType));
        return registrator;
    }

    /// <summary>
    /// 移除注册信息
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="fromType"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator Unregister(this IRegistrator registrator, Type fromType, string key)
    {
        registrator.Unregister(new DependencyDescriptor(fromType), key);
        return registrator;
    }

    /// <summary>
    /// 移除注册信息
    /// </summary>
    /// <param name="registrator"></param>
    /// <returns></returns>
    public static IRegistrator Unregister<TFrom>(this IRegistrator registrator)
    {
        registrator.Unregister(new DependencyDescriptor(typeof(TFrom)));
        return registrator;
    }

    /// <summary>
    /// 移除注册信息
    /// </summary>
    /// <param name="registrator"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IRegistrator Unregister<TFrom>(this IRegistrator registrator, string key)
    {
        registrator.Unregister(new DependencyDescriptor(typeof(TFrom)), key);
        return registrator;
    }

    #endregion Unregister

    #region Resolve

    /// <summary>
    /// 创建类型对应的实例。如果解析失败，则返回 null。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resolver"></param>
    /// <returns></returns>
    public static T Resolve<[DynamicallyAccessedMembers(DynamicallyAccessed)] T>(this IServiceProvider resolver)
    {
        return (T)resolver.GetService(typeof(T));
    }

    /// <summary>
    /// 创建类型对应的实例。如果解析失败，则返回 null。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resolver"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T Resolve<[DynamicallyAccessedMembers(DynamicallyAccessed)] T>(this IResolver resolver, string key)
    {
        return (T)resolver.Resolve(typeof(T), key);
    }

    /// <summary>
    /// 创建<see cref="Lifetime.Transient"/>生命的未注册的根类型实例。一般适用于：目标类型没有注册，但是其成员类型已经注册的情况。
    /// </summary>
    /// <param name="resolver"></param>
    /// <param name="fromType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static object ResolveWithoutRoot(this IServiceProvider resolver, [DynamicallyAccessedMembers(DynamicallyAccessed)] Type fromType)
    {
        object[] ops = null;
        var ctor = fromType.GetConstructors().FirstOrDefault(x => x.IsDefined(typeof(DependencyInjectAttribute), true));
        if (ctor is null)
        {
            //如果没有被特性标记，那就取构造函数参数最多的作为注入目标
            if (fromType.GetConstructors().Length == 0)
            {
                throw new Exception(TouchSocketCoreResource.NotFindPublicConstructor.Format(fromType));
            }
            ctor = fromType.GetConstructors().OrderByDescending(x => x.GetParameters().Length).First();
        }
        DependencyTypeAttribute dependencyTypeAttribute = null;
        if (fromType.IsDefined(typeof(DependencyTypeAttribute), true))
        {
            dependencyTypeAttribute = fromType.GetCustomAttribute<DependencyTypeAttribute>();
        }

        var parameters = ctor.GetParameters();
        var ps = new object[parameters.Length];

        if (dependencyTypeAttribute == null || dependencyTypeAttribute.Type.HasFlag(DependencyType.Constructor))
        {
            for (var i = 0; i < parameters.Length; i++)
            {
                if (ops != null && ops.Length - 1 >= i)
                {
                    ps[i] = ops[i];
                }
                else
                {
                    if (parameters[i].ParameterType.IsPrimitive || parameters[i].ParameterType == typeof(string))
                    {
                        ps[i] = parameters[i].HasDefaultValue ? parameters[i].DefaultValue : default;
                    }
                    else
                    {
                        if (parameters[i].IsDefined(typeof(DependencyInjectAttribute), true))
                        {
                            var attribute = parameters[i].GetCustomAttribute<DependencyInjectAttribute>();
                            var type = attribute.Type ?? parameters[i].ParameterType;
                            ps[i] = resolver.GetService(type);
                        }
                        else
                        {
                            ps[i] = resolver.GetService(parameters[i].ParameterType);
                        }
                    }
                }
            }
        }
        return ps == null || ps.Length == 0 ? Activator.CreateInstance(fromType) : Activator.CreateInstance(fromType, ps);
    }

    /// <summary>
    /// 创建<see cref="Lifetime.Transient"/>生命的未注册的根类型实例。一般适用于：目标类型没有注册，但是其成员类型已经注册的情况。
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static T ResolveWithoutRoot<[DynamicallyAccessedMembers(DynamicallyAccessed)] T>(this IServiceProvider resolver)
    {
        return (T)ResolveWithoutRoot(resolver, typeof(T));
    }
    #endregion Resolve

    #region IsRegistered

    /// <summary>
    /// <inheritdoc cref="IRegistered.IsRegistered(Type)"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="registered"></param>
    /// <returns></returns>
    public static bool IsRegistered<[DynamicallyAccessedMembers(DynamicallyAccessed)] T>(this IRegistered registered)
    {
        return registered.IsRegistered(typeof(T));
    }

    /// <summary>
    /// <inheritdoc cref="IRegistered.IsRegistered(Type, string)"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="registered"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsRegistered<[DynamicallyAccessedMembers(DynamicallyAccessed)] T>(this IRegistered registered, string key)
    {
        return registered.IsRegistered(typeof(T), key);
    }

    #endregion IsRegistered
}