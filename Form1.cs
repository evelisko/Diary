using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Data.Sql; 


namespace Diary
{
    public partial class Form1 : Form
    {
        int CurrentMonth, CurrentYear, CurrentDay;
        static string[] Months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        static string strDataBaseName = "TestDataBase"; // Название базы данных.
        private Color _color = Color.Green;
     
     // Создаем динамический массив с ID номерами всех ячеек на текущий день. 
     // Добавляем и удалаяем значения из массива при редактировании таблицы. 
     // м.б имеет смысл создать еще одну таблицу. которая бы хранила все значения в тени. 
     // DataTable. 

        int[,] DayWitchTasks;
        int[] taskIDs;
        int newIdIndex; // индекс следующейстроки. которая будет создана.  


        public Form1()
        {
            InitializeComponent();
            btnDeleteTask.Visible = false;
            btnTaskModify.Visible = false;
            tmrCurentTime.Enabled = true;
            dGvTasks.AllowUserToAddRows = false;
            dGvTasks.AllowUserToDeleteRows = false;
            dGvTasks.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dGvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dGvTasks.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dGvTasks.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            DayWitchTasks = new int [7,6]; // массив содержащий  информацию о текущих днях месяца для которых есть задачи. 

         // Оформить все операции выполняемые над БД в виде отдельных процедур. 
         // создаем ячейки. 
         // задаем ширину колонок 
         // Подсвечивать дни на которые созданы задачи. 

            dGvTasks.Columns[0].Width = 90;
            dGvTasks.Columns[1].Width = 200;

         // CalendarInitialize();

            CurrentMonth = DateTime.Now.Month;
            CurrentYear = DateTime.Now.Year;
            CurrentDay = DateTime.Now.Day;

            CalendarRefresh(CurrentYear, CurrentMonth);

            // производим проверку. Есть ли задачи сегодня.  
            // подключаемся к базе данных Ежедневника
            // Если база данных еще не была создана - создаем базу данных. 
            CreatDataBase();
            // Проверяем создана ли таблица текущих дел. создаем новую если таковой еще нет. 
            // считываем дела на текущий месяц. 
            // считываем дела на текущий день. 
            ShowFields();

            lvTaskList.View = View.Details;
            lvTaskList.BorderStyle = BorderStyle.None;
            lvTaskList.FullRowSelect = true;
            ListViewItem lvItem = new ListViewItem(new string[] { "11", "22", "33" });
            lvTaskList.Items.Add(lvItem);
     
               // listView1
     /*     
                  listviewitem item = new listviewitem(dr.getvalue(0).tostring());
                  item.subitems.add(dr.getvalue(1).tostring());
                  item.subitems.add(dr.getvalue(2).tostring());
                  item.subitems.add(dr.getvalue(3).tostring());
                  item.subitems.add(dr.getvalue(4).tostring());
                  item.subitems.add(dr.getvalue(5).tostring());
                  item.subitems.add(dr.getvalue(6).tostring());
                  item.subitems.add(dr.getvalue(7).tostring());
                  item.subitems.add(dr.getvalue(8).tostring());
                  listview1.items.add(item);
     */
        }

        public void CalendarRefresh(int setYear, int setMonth)
        {
            dGvCalendar.RowTemplate.Height = (int)(Math.Truncate((double)(dGvCalendar.Height - dGvCalendar.ColumnHeadersHeight) / 6));
            dGvCalendar.RowCount = 6;
            int i, j, k, daysInMounth, daysInMounthLast, lastmonth, lastYear;
            int lastDay = 0;
            //k = 0;
            j = 0;
            i = 0;

     //   dGvCalendar.DefaultCellStyle.dGvCalendar.Width; 
     //   dGvCalendar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
     //   dGvCalendar.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
     //   dGvCalendar.DefaultCellStyle.Font = n

            lastmonth = setMonth - 1;
            lastYear = setYear;
            if (lastmonth < 1) { lastmonth = 12; lastYear--; }
            daysInMounthLast = DateTime.DaysInMonth(lastYear, lastmonth);
            //while 
            // При запуске программы показываем задачи только на текущий месяц
            // Установим цикл While 


            lblCurrentMonth.Text = Months[setMonth - 1] + " " + setYear.ToString("0000");

            DateTime dateValue = new DateTime(setYear, setMonth, 1);
            j = ((int)dateValue.DayOfWeek) - 1;
            if (j == -1) j = 6;

            // Задаем текущую активную ячейку в таблице. 
            // dGvCalendar.CurrentCell = ;//.SelStyle.ForeColor = Color.Black; dGvCalendar[j, i].Value = k.ToString();
            // dGvCalendar.CurrentCell = DataGridView[i, j]; 

            lastDay = j - 1;

            while (lastDay > -1)
            {
                dGvCalendar[lastDay, 0].Value = daysInMounthLast.ToString();
                dGvCalendar.Rows[0].Cells[lastDay].Style.ForeColor = Color.DarkSlateGray;
                lastDay--;
                daysInMounthLast--;
            }


            i = 0;
            daysInMounth = DateTime.DaysInMonth(setYear, setMonth);

            // Предыдущий месяц 
            // задаем цвет для ячеек предыдущего месяца. 
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //  CurrentDay = Convert.ToInt32(dGvCalendar.CurrentCell.Value);

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                statusLabel.Text = "Подключение к БД Diary";
                //пробуем подключится
                conn.Open();
            }

            catch (SqlException se)
            {
                // Если база не обнаружена, то создаем новую
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    // закрываем соединение
                    conn.Close();
                    return;

                }
            }

         
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            k = 1;

            while (k < daysInMounth + 1)
            {
                if (j > dGvCalendar.ColumnCount - 1) { i++; j = 0; }

       

             // производим инициализацию списка с задачами 
             /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
             // Выводим значение на экран
                using (SqlCommand cmd = new SqlCommand("Select COUNT(*) From Diary WHERE Day = @Day AND Month = @Month AND Year = @Year", conn))
                {

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@Day"; param.Value = k; param.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter();
                    param.ParameterName = "@Month"; param.Value = CurrentMonth; param.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter();
                    param.ParameterName = "@Year"; param.Value = CurrentYear; param.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(param);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                       while(dr.Read())
                           DayWitchTasks[j, i] =Convert.ToInt32(dr.GetValue(0).ToString());
                    }
                    // Производим сохранение задачи в базе данных 

                    dGvCalendar[j, i].Value = k.ToString();// + " " + DayWitchTasks[j, i].ToString();
                    dGvCalendar.Rows[i].Cells[j].Style.ForeColor = Color.Black;

                    if (CurrentDay == k)
                        dGvCalendar[j, i].Selected = true;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    k++;
                    j++;

                    //    ShowFields();
                }
            }
              conn.Close();
              conn.Dispose();
     // Последующий месяц
            k = 1;
            while (j < 7)
            {
                dGvCalendar[j, i].Value = k.ToString();
                // задаем цвет для ячеек предыдущего месяца. 
                dGvCalendar.Rows[i].Cells[j].Style.ForeColor = Color.DarkSlateGray;
                j++;
                k++;
            }

            if (i < dGvCalendar.RowCount - 1)
            {
                i++;
                j = 0;
                while (j < 7)
                {
                    dGvCalendar[j, i].Value = k.ToString();
                    dGvCalendar.Rows[i].Cells[j].Style.ForeColor = Color.DarkSlateGray;

                    j++;
                    k++;
                }
            }
        }


        private void tmrCurentTime_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
        }

        public void CalendarInitialize()
        {
            // создадим таблицу вывода товаров с колонками
            // Название, Цена, Остаток

            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Название"; // текст в шапке
            column1.Width = 100;     // ширина колонки
            column1.ReadOnly = true; // значение в этой колонке нельзя править
            column1.Name = "name";   // текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.Frozen = true;   // флаг, что данная колонка всегда отображается на своем месте
            column1.CellTemplate = new DataGridViewTextBoxCell(); // тип нашей колонки

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Цена";
            column2.Name = "price";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Остаток";
            column3.Name = "count";
            column3.CellTemplate = new DataGridViewTextBoxCell();

        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            if (CurrentMonth == 12)
            {
                CurrentYear++;
                CurrentMonth = 1;
            }
            else
            {
                CurrentMonth++;
            }
            CalendarRefresh(CurrentYear, CurrentMonth);
        }

        private void btnRedoMonth_Click(object sender, EventArgs e)
        {
            if (CurrentMonth == 1)
            {
                CurrentYear--;
                CurrentMonth = 12;
            }
            else
            {
                CurrentMonth--;
            }
            CalendarRefresh(CurrentYear, CurrentMonth);
        }

        private void dGvCalendar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowFields();
        }

        private void CreatDataBase()
        {
         /* Объявляем строковую переменную и записываем в нее
                строку подключения 
                Data Source - имя сервера, по стандарту (local)\SQLEXPRESS
                Initial Catalog - имя БД 
                Integrated Security=-параметры безопасности
                Мое подключение имеет вид 
         */

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));
            //    string connStr = @"Data Source=(local)\SQLEXPRESS; Initial Catalog= TestDatBase; Integrated Security=True";

            /*Здесь указал имя БД(хотя для создания БД его указывать не нужно)
              для того, чтобы проверить, может данная БД уже создана
            Создаем экземпляр класса  SqlConnection по имени conn
            и передаем конструктору этого класса, строку подключения
         */
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                statusLabel.Text = "Подключение к БД Diary";
                //пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                // Если база не обнаружена, то создаем новую
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Подождите, идет создание БД";
                    // закрываем соединение
                    conn.Close();
                    // переопределяем обьект conn, и передаем новую строку подключения
                    conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Integrated Security=True");
                    /*Создаем экземпляр класса  SqlCommand по имени cmdCreateDataBase
                     и передаем конструктору этого класса, запрос на создание БД
                     и объект типа SqlConnection
                      */
                    SqlCommand cmdCreateDataBase = new SqlCommand(string.Format("CREATE DATABASE [{0}]", strDataBaseName), conn);
                    //открываем подключение
                    conn.Open();
                    /*Посылаем запрос к СУБД
                     В данном случае, в результате запроса ничего не возврашается
                     ExecuteNonQuery, в последующих примерах мы будем использовать
                     и другие методы
                     */
                    statusLabel.Text = "Посылаем запрос";
                    cmdCreateDataBase.ExecuteNonQuery();
                    //закрываем подключение
                    conn.Close();
                    //задержка, нужна для того, чтоб БД успела создаться
                    Thread.Sleep(5000);
                    //переопределяем обьект conn, и передаем новую строку подключения
                    conn = new SqlConnection(connStr);
                    //открываем подключение
                    conn.Open();
                }
            }
            //       conn.Close();

            statusLabel.Text = "Соедение успешно произведено ";

            //      SqlCommand cmdCreateTable = new SqlCommand("CREATE TABLE " + 
            //                                                 "Students (ID int not null" +
            //                                                 ", FIO char(60) not null," +
            //                                                 "  Grupa char(20) not null)", conn);
            //посылаем запрос
            //      try
            //      {
            //          cmdCreateTable.ExecuteNonQuery();
            //      }
            //      catch
            //      {
            //          textBox1.AppendText("Ошибка при создании таблицы \r\n");
            //          return;
            //      }

            //       textBox1.AppendText("Таблица создана успешно\r\n");
            //        закрвываем соединение
            //       conn.Close();
            //       conn.Dispose();
            try  // Проверяем созданна ли таблица 
            {
                SqlCommand cmdCreateTableBase = new SqlCommand("Select * From Diary", conn);
                cmdCreateTableBase.ExecuteNonQuery();//  SqlDataReader dr = cmd.ExecuteReader();
                statusLabel.Text = "Таблица Подключена";
            }
            catch (SqlException se)
            {
                if (se.Number == 208)
                    CreateNewTable(conn);
            }
            conn.Close();
            conn.Dispose();
            //   }

        }

        private void DeleteTable(SqlConnection conn)
        {
          using (SqlCommand cmdDeleteTable = new SqlCommand("DROP TABLE Diary", conn))
            {
           // Посылаем запрос
                try
                {
                    cmdDeleteTable.ExecuteNonQuery();
                }
                catch
                {
                    statusLabel.Text = "Ошибка при удалении таблицы";
                    return;
                }
            }
            statusLabel.Text = "Таблица удалена успешно";
        }


        private void DeleteTask() // При выполнении каждой процедуры производится подключение к базе данных и отключение от не после завершения запроса.       
        {
            CurrentDay = Convert.ToInt32(dGvCalendar.CurrentCell.Value);

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
              statusLabel.Text = "Подключение к БД Diary";
         // пробуем подключится
              conn.Open();
            }
            catch (SqlException se)
            {
                // Если база не обнаружена, то создаем новую
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    // закрываем соединение
                    conn.Close();
                    return;
                }
            }

            // нужно производить еще дополнительнуюю сортировку по ID. 
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Diary WHERE ID = @ID AND Day = @Day AND Month = @Month AND Year = @Year", conn))
            {
             SqlParameter param = new SqlParameter();
                // задаем имя параметра
                param.ParameterName = "@ID"; param.Value = (taskIDs[dGvTasks.CurrentRow.Index]); param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Day"; param.Value = Convert.ToInt16(dGvCalendar.CurrentCell.Value); param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Month"; param.Value = CurrentMonth; param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Year"; param.Value = CurrentYear; param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
                        
                statusLabel.Text = "Удаление записи";

                try
                {
                  cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                  statusLabel.Text = "Ошибка, при выполнении запроса на удаление записи";

             // закрываем соединение
                  conn.Close();
                  return;
                }
            }
            conn.Close();
            conn.Dispose();
         if  (DayWitchTasks[dGvCalendar.CurrentCell.ColumnIndex, dGvCalendar.CurrentCell.RowIndex]> 0 )
                    DayWitchTasks[dGvCalendar.CurrentCell.ColumnIndex, dGvCalendar.CurrentCell.RowIndex]--;
          
            ShowFields();
        }

        private void TaskModify()
        {
            int taskID = 0; 
            CurrentDay = Convert.ToInt32(dGvCalendar.CurrentCell.Value);

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));

            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                statusLabel.Text = "Подключение к БД Diary";
             // пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                // Если база не обнаружена, то создаем новую
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    // закрываем соединение
                    conn.Close();
                    return;
                }
            }

        // Выводим значение на экран
            SqlCommand cmd = new SqlCommand("Select * From Diary WHERE Day = @Day AND Month = @Month AND Year = @Year", conn);

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Day"; param.Value = CurrentDay; param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@Month"; param.Value = CurrentMonth; param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@Year"; param.Value = CurrentYear; param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

           SqlDataReader dr = cmd.ExecuteReader();
   
           AddTaskDialog taskDialog = new AddTaskDialog();
            int i = 0;
            while (dr.Read())
            {
                if (i == dGvTasks.CurrentRow.Index)
                {
                    taskID =Convert.ToInt32((dr.GetValue(0).ToString()).Trim());
                    taskDialog.TaskName = dr.GetValue(5).ToString();
                    taskDialog.TaskType = dr.GetValue(4).ToString();
                    taskDialog.TaskLocation = dr.GetValue(8).ToString();
                    taskDialog.StartTime = dr.GetValue(6).ToString();
                    taskDialog.EndTime = dr.GetValue(7).ToString();
                    break;
                }
                i++;
            }

            conn.Close();

            taskDialog.ShowDialog();

            if (taskDialog.DialogResult != DialogResult.OK) return;

            string strTaskName = taskDialog.TaskName;
            string strTaskType = taskDialog.TaskType;
            string strTaskLocation = taskDialog.TaskLocation;
            string strStartTime = taskDialog.StartTime;
            string strEndTime = taskDialog.EndTime;

            // Произодим запись новой задачи в базу данных. 

            taskDialog.Close();

            try
            {
                statusLabel.Text = "Подключение к БД Diary ";
                //пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                // Если база не обнаружена, то создаем новую
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    // закрываем соединение
                    conn.Close();
                    return;
                }
            }

     // нужно производить еще дополнительнуюю сортировку по ID. 
            cmd = new SqlCommand("UPDATE Diary Set TaskType = @TaskType , TaskName = @TaskName , StartTime = @StartTime , EndTime = @EndTime , Location =  @Location WHERE ID = @ID AND Day = @Day AND Month = @Month AND Year = @Year", conn);

            param = new SqlParameter();
     // задаем имя параметра
            param.ParameterName = "@ID"; param.Value = (taskID); param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@Day"; param.Value = Convert.ToInt16(dGvCalendar.CurrentCell.Value); param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@Month"; param.Value = CurrentMonth; param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@Year"; param.Value = CurrentYear; param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@TaskType"; param.Value = strTaskType; param.SqlDbType = SqlDbType.Text;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@TaskName"; param.Value = strTaskName; param.SqlDbType = SqlDbType.Text;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@StartTime"; param.Value = strStartTime; param.SqlDbType = SqlDbType.Text;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@EndTime"; param.Value = strEndTime; param.SqlDbType = SqlDbType.Text;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@Location"; param.Value = strTaskLocation; param.SqlDbType = SqlDbType.Text;
            cmd.Parameters.Add(param);

            statusLabel.Text = "Измененение записи";

            try
            {
              cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                statusLabel.Text = "Ошибка, при выполнении запроса на изменение записи";
         // закрываем соединение
                conn.Close();
                return;
            }

            conn.Close();
            conn.Dispose();

            // Добавляем запись в таблицу просмотра. 
            if ((strStartTime == "00:00") && ( strEndTime == "00:00"))
            {
                dGvTasks[0, dGvTasks.CurrentRow.Index].Value = "Весь день";
            }
            else
            if (strEndTime == "00:00")
            {
              dGvTasks[0, dGvTasks.CurrentRow.Index].Value =  strStartTime;
            }
            else
            {
              dGvTasks[0, dGvTasks.CurrentRow.Index].Value = strStartTime + "\r\n" + strEndTime;
            }

            //  dGvCalendar.Rows[dGvTasks.Rows.Count - 1].Cells[1].Style.ForeColor = Color.Black;

            dGvTasks[1, dGvTasks.CurrentRow.Index].Value = strTaskType + "\r\n" + strTaskName + "\r\n" + strTaskLocation;
        }


        private void ShowFields()
        {
            int i = 0;
            CurrentDay = Convert.ToInt32(dGvCalendar.CurrentCell.Value);

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                statusLabel.Text = "Подключение к БД Diary";
                //пробуем подключится
                conn.Open();
            }

            catch (SqlException se)
            {
                // Если база не обнаружена, то создаем новую
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    // закрываем соединение
                    conn.Close();
                    return;

                }
            }

         // Выводим значение на экран
            using (SqlCommand cmd = new SqlCommand("Select * From Diary WHERE Day = @Day AND Month = @Month AND Year = @Year", conn))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Day"; param.Value = CurrentDay; param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Month"; param.Value = CurrentMonth; param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Year"; param.Value = CurrentYear; param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                  if (dr.FieldCount < 1)
                    {
             //   textBox1.AppendText("на " + CurrentDay.ToString()+ " "+ Months[CurrentMonth]+" задач нет");
                        conn.Close();
                        return;
                    }

                  lvTaskList.Items.Clear();

                    // цикл по всем столбцам полученной в результате запроса таблицы

                    taskIDs = new int[DayWitchTasks[dGvCalendar.CurrentCell.ColumnIndex, dGvCalendar.CurrentCell.RowIndex]]; 


                    dGvTasks.Rows.Clear();
                    //     dGvTasks.RowCount = 6;
                    //        taskIDs = new int[dr];  
                   i = 0; 
                   while (dr.Read())
                    {
                      ListViewItem item = new ListViewItem(dr.GetValue(6).ToString() + "\r\n" + dr.GetValue(7).ToString());
                 //   item.SubItems.Add(dr.GetValue(6).ToString() + "\r\n" + dr.GetValue(7).ToString());
                        item.SubItems.Add(dr.GetValue(5).ToString());
                 /*                    
                        item.SubItems.Add(dr.GetValue(1).ToString());
                        item.SubItems.Add(dr.GetValue(2).ToString());
                        item.SubItems.Add(dr.GetValue(3).ToString());
                        item.SubItems.Add(dr.GetValue(4).ToString());
                        item.SubItems.Add(dr.GetValue(5).ToString());
                        item.SubItems.Add(dr.GetValue(6).ToString());
                        item.SubItems.Add(dr.GetValue(7).ToString());
                        itItem.em.SubItems.Add(dr.GetValue(8).ToString());
                 */

                        lvTaskList.Items.Add(item);

                    if (taskIDs.Length > 0) taskIDs[i] = Convert.ToInt32(dr.GetValue(0).ToString()); // динамические массивы картежи
                        i ++;

                 // нужно заново переиинициализировать таблицу. 
                        dGvTasks.Rows.Add(1);
                    
                      if ((dr.GetValue(6).ToString() == "00:00") && (dr.GetValue(7).ToString() == "00:00"))
                        {
                            dGvTasks[0, dGvTasks.Rows.Count - 1].Value = dr.GetValue(4).ToString()+ "\r\n"+ "Весь день";
                        }
                        else
                      if ((dr.GetValue(7).ToString() == "00:00"))
                        {
                          dGvTasks[0, dGvTasks.Rows.Count - 1].Value = dr.GetValue(4).ToString() + "\r\n" + dr.GetValue(6).ToString();
                        }
                        else
                        {
                          dGvTasks[0, dGvTasks.Rows.Count - 1].Value = dr.GetValue(4).ToString() + "\r\n" + dr.GetValue(6).ToString() + "\r\n" + dr.GetValue(7).ToString();
                        }

                    // dGvCalendar.Rows[dGvTasks.Rows.Count - 1].Cells[1].Style.ForeColor = Color.Black;

                        dGvTasks[1, dGvTasks.Rows.Count - 1].Value = dr.GetValue(5).ToString() + "\r\n" + dr.GetValue(8).ToString();

                    // dGvCalendar.Rows[dGvTasks.Rows.Count - 1].Cells[1].Style.ForeColor = Color.Black;

                        dGvTasks.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                     // CurrentRow.Height = 5 + dGvTasks.CurrentRow.GetPreferredHeight(dGvTasks.Rows.Count - 1,DataGridViewAutoSizeRowMode.AllCells,true); 
                     // dGvCalendar.Rows[].Cells[].Style.ForeColor = Color.Black;


                        //"(ID int not null, " +                  0
                        //"Day int not null, " +                  1  
                        //"Month int not null, " +                2
                        //"Year int not null, " +                 3
                        //"TaskType text not null, " + //, conn)) 4
                        //"TaskName text not null, " +            5
                        //"StartTime text not null, " +           6
                        //"EndTime text not null, " +             7
                        //"Location text not null)" , conn))      8

                        // будем заполнять dataGrid.
                    }
                }
            }

            ////////////////////////////////////////////////////////////////////////////////

            // Производим сохранение задачи в базе данных 
            conn.Close();
            conn.Dispose();
            // Добавляем запись в таблицу просмотра. 
            
            newIdIndex = 0;

          for (i = 0; i < taskIDs.Length; i++)
            {
                if (newIdIndex <= taskIDs[i]) newIdIndex = taskIDs[i] + 1; 
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (dGvTasks.RowCount > 0)
            {
                btnDeleteTask.Visible = true;
                btnTaskModify.Visible = true;  
            }
            else
            {
                btnDeleteTask.Visible = false;
                btnTaskModify.Visible = false;
            }
        }

        private void CreateNewTable(SqlConnection conn)
        {
            using (SqlCommand cmdCreateTable = new SqlCommand("CREATE TABLE " + " Diary " +
               "(ID int not null, " +
               "Day int not null, " +
               "Month int not null, " +
               "Year int not null, " +
               "TaskType text not null, " + //, conn))
               "TaskName text not null, " +
               "StartTime text not null, " +
               "EndTime text not null, " +
               "Location text not null)", conn))
            {
                // посылаем запрос
                try
                {
                    cmdCreateTable.ExecuteNonQuery();
                }
                catch
                {
                    statusLabel.Text = "Ошибка при создании таблицы";
                    return;
                }
            }

            statusLabel.Text = "Таблица создана успешно";
        }

        private void btnTaskModify_Click(object sender, EventArgs e)
        {
           TaskModify();
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            DeleteTask();      
        }

        private void dGvCalendar_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
         // Draw only grid content cells not ColumnHeader cells nor RowHeader cells
            if (e.ColumnIndex > -1 & e.RowIndex > -1)
            {
             // Pen for left and top borders
                using (var backGroundPen = new Pen(e.CellStyle.BackColor, 2))
             // Pen for bottom and right borders
                using (var gridlinePen = new Pen(dGvCalendar.GridColor, 2))
             // Pen for selected cell borders
                using (var selectedPen = new Pen(Color.LightBlue, 1))
                {
                    var topLeftPoint = new Point(e.CellBounds.Left, e.CellBounds.Top);
                    var topRightPoint = new Point(e.CellBounds.Right, e.CellBounds.Top);
                    var bottomRightPoint = new Point(e.CellBounds.Right, e.CellBounds.Bottom);
                    var bottomleftPoint = new Point(e.CellBounds.Left, e.CellBounds.Bottom);

                 // Draw selected cells here
                    if (this.dGvCalendar[e.ColumnIndex, e.RowIndex].Selected)
                    {
                     // Paint all parts except borders.
                       e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                       
                     // Draw selected cells border here
                        e.Graphics.DrawRectangle(selectedPen, new Rectangle(e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Width , e.CellBounds.Height));
                       
                   if(DayWitchTasks[e.ColumnIndex,e.RowIndex]> 0)     e.Graphics.DrawRectangle(selectedPen, new Rectangle(e.CellBounds.Left + e.CellBounds.Width/2-4, e.CellBounds.Top + 5,5,5));

                     // Handled painting for this cell, Stop default rendering.
                        e.Handled = true;
                    }
                 // Draw non-selected cells here
                    else
                    {
                     // Paint all parts except borders.
                        e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                     // Top border of first row cells should be in background color
                        if (e.RowIndex == 0)
                            e.Graphics.DrawLine(backGroundPen, topLeftPoint, topRightPoint);

                     // Left border of first column cells should be in background color
                        if (e.ColumnIndex == 0)
                            e.Graphics.DrawLine(backGroundPen, topLeftPoint, bottomleftPoint);

                     // Bottom border of last row cells should be in gridLine color
                        if (e.RowIndex == dGvCalendar.RowCount - 1)
                            e.Graphics.DrawLine(gridlinePen, bottomRightPoint, bottomleftPoint);
                        else  //Bottom border of non-last row cells should be in background color
                            e.Graphics.DrawLine(backGroundPen, bottomRightPoint, bottomleftPoint);

                     // Right border of last column cells should be in gridLine color
                        if (e.ColumnIndex == dGvCalendar.ColumnCount - 1)
                            e.Graphics.DrawLine(gridlinePen, bottomRightPoint, topRightPoint);
                        else //Right border of non-last column cells should be in background color
                            e.Graphics.DrawLine(backGroundPen, bottomRightPoint, topRightPoint);

                     // Top border of non-first row cells should be in gridLine color, and they should be drawn here after right border
                        if (e.RowIndex > 0)
                            e.Graphics.DrawLine(gridlinePen, topLeftPoint, topRightPoint);

                     // Left border of non-first column cells should be in gridLine color, and they should be drawn here after bottom border
                        if (e.ColumnIndex > 0)
                            e.Graphics.DrawLine(gridlinePen, topLeftPoint, bottomleftPoint);

                        if (DayWitchTasks[e.ColumnIndex, e.RowIndex] > 0) e.Graphics.DrawRectangle(selectedPen, new Rectangle(e.CellBounds.Left + e.CellBounds.Width / 2 - 4, e.CellBounds.Top + 5, 5, 5));

                        // We handled painting for this cell, Stop default rendering.
                        e.Handled = true;
                    }
                }
            }
        }

        private void AddTask()
        {
           // int  taskID = dGvTasks.RowCount; 
            AddTaskDialog taskDialog = new AddTaskDialog();

            taskDialog.ShowDialog();
            if (taskDialog.DialogResult != DialogResult.OK) return;

            string strTaskName = taskDialog.TaskName;
            string strTaskType = taskDialog.TaskType;
            string strTaskLocation = taskDialog.TaskLocation;
            string strStartTime = taskDialog.StartTime;
            string strEndTime = taskDialog.EndTime;

            // Произодим запись новой задачи в базу данных. 

            taskDialog.Close();

            // Производим сохранение задачи в базе данных 

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                statusLabel.Text = "Подключение к БД Diary ";
                //пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                // Если база не обнаружена, то создаем новую
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    // закрываем соединение
                    conn.Close();
                    return;

                }
            }
            // узнаем сколько на текущий день задачь уже существует. 

            using (SqlCommand cmd = new SqlCommand("INSERT INTO Diary (ID,Day,Month,Year,TaskType,TaskName,StartTime,EndTime,Location) Values (@ID,@Day,@Month,@Year,@TaskType,@TaskName,@StartTime,@EndTime,@Location)", conn))
            {
                /*Работаем с параметрами(SqlParameter), эта техника позволяет уменьшить
                кол-во ошибок и достичь большего быстродействия
                 но требует и больших усилий в написании кода*/

                // объявляем объект класса SqlParameter
                SqlParameter param = new SqlParameter();
                // задаем имя параметра
                param.ParameterName = "@ID"; param.Value = newIdIndex; param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Day"; param.Value = Convert.ToInt16(dGvCalendar.CurrentCell.Value); param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Month"; param.Value = CurrentMonth; param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Year"; param.Value = CurrentYear; param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@TaskType"; param.Value = strTaskType; param.SqlDbType = SqlDbType.Text;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@TaskName"; param.Value = strTaskName; param.SqlDbType = SqlDbType.Text;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@StartTime"; param.Value = strStartTime; param.SqlDbType = SqlDbType.Text;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@EndTime"; param.Value = strEndTime; param.SqlDbType = SqlDbType.Text;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Location"; param.Value = strTaskLocation; param.SqlDbType = SqlDbType.Text;
                cmd.Parameters.Add(param);

                statusLabel.Text = "Вставляем запись";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    statusLabel.Text = "Ошибка, при выполнении запроса на добавление записи";
                    // закрываем соединение
                    conn.Close();
                    return;
                }

            }

            conn.Close();
            conn.Dispose();
    // Добавляем запись в таблицу просмотра.
           DayWitchTasks[dGvCalendar.CurrentCell.ColumnIndex, dGvCalendar.CurrentCell.RowIndex] ++ ;//= DayWitchTasks[dGvCalendar.CurrentCell.ColumnIndex, dGvCalendar.CurrentCell.RowIndex]]
           newIdIndex++;
           ShowFields(); 
        }

      private void btnAddTask_Click(object sender, EventArgs e)
       {
          AddTask();
       }

    }
}
