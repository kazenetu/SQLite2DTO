using Domain.Classes;
using System.Collections.Generic;

namespace Domain.DB
{
  /// <summary>
  /// DBリポジトリ インターフェイス
  /// </summary>
  public interface IDBRepository
  {
    /// <summary>
    /// DBからClassEntityリストを取得する
    /// </summary>
    /// <returns>ClassEntityリスト</returns>
    List<ClassEntity> GetClasses(DBParameterEntity parameterEntity);
  }
}