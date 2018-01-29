using System;
using System.Collections.Generic;
using DiakontTestProject.Model;

namespace DiakontTestProject.Data
{
    /// <summary>
    /// Основной репозиторий работы с данными
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Получение списка ставок
        /// </summary>
        /// <returns></returns>
        IEnumerable<Rate> GetRates();

        /// <summary>
        /// Получение списка штатного расписания
        /// </summary>
        /// <returns></returns>
        IEnumerable<SchedulePosition> GetSchedule();

        /// <summary>
        /// Получение отчета о ФОТ
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        IEnumerable<SearchResult> FindFundOfSalary(DateTime from, DateTime to);

        /// <summary>
        /// Добавление ставки
        /// </summary>
        /// <param name="rate"></param>
        void AddRate(Rate rate);

        /// <summary>
        /// Добавление штатного расписания
        /// </summary>
        /// <param name="schedulePosition"></param>
        void AddSchedulePosition(SchedulePosition schedulePosition);
    }
}
