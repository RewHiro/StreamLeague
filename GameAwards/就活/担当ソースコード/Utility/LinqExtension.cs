/// <author>
/// 新井大一
/// </author>

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Linq機能の拡張
/// </summary>
public static class LinqExtension
{
    /// <summary>
    /// 全ての配列に対してその関数を実行する
    /// </summary>
    /// <typeparam name="TSource">sourceの要素の型</typeparam>
    /// <param name="sources">sourceの要素</param>
    /// <param name="action">TSourceにある実行したい関数</param>
    /// <returns>sourceの要素</returns>
    public static IEnumerable<TSource> ExecuteAction<TSource>(this IEnumerable<TSource>sources, Action<TSource>action)
    {
        foreach (var source in sources)
        {
            action(source);
        }

        return sources;
    }


    public static Transform NearestObject(this IEnumerable<GameObject> sources, Vector3 position)
    {
        if (sources.ToArray().Count() == 0) return null;
        var result =
            sources.OrderBy
            (
                game_object => Vector3.Distance
                (
                    game_object.transform.position,
                    position
                )
            );

        return result.ToArray()[0].transform;
    }

    public static Transform ObjectCloseToTheSecond(this IEnumerable<GameObject> sources, Vector3 position)
    {
        if (sources.ToArray().Count() <= 1) return null;
        var result =
            sources.OrderBy
            (
                game_object => Vector3.Distance
                (
                    game_object.transform.position,
                    position
                )
            );

        return result.ToArray()[1].transform;
    }
}