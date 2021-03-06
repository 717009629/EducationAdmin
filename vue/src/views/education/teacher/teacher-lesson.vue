<template>
  <div>
    <Modal :value="value" @on-visible-change="visibleChange" :mask-closable="false" width="1200px">
      <div slot="header">
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{L('Lesson')}}</span>
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{teacher.name}}</span>
        <Button @click="calenderShow=!calenderShow" size='small'>{{calenderShow? L("List"):L("Calendar")}}</Button>
      </div>
      <div>
        <FullCalendar ref='calendar' v-if="calenderShow&&value" defaultView="timeGridWeek" :plugins="calendarPlugins" :locale="locale" :events='events' :displayEventTime='true' :eventLimit='true'
                      @eventClick='eventClick' :allDaySlot='false' minTime='07:00:00' maxTime='21:00:00' slotDuration='00:15:00' slotLabelInterval='01:00'
                      :header="{left:'title',center:'',right:'timeGridWeek, dayGridMonth today prev,next'}" :buttonText="{today:L('Today'),month:L('Month'),week:L('Week'),}">
        </FullCalendar>
        <!-- <Card dis-hover> -->
        <div v-if="!calenderShow">
          <Form ref="queryForm" :label-width="100" label-position="left" inline>

          </Form>
          <div class="margin-top-10">
            <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">

            </Table>
            <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
            </Page>
          </div>

        </div>
      </div>
      <div slot="footer"></div>
      <create-lesson-attendance v-model="createLessonAttendaceModalShow" @save-success="refetchEvents"></create-lesson-attendance>
      <edit-lesson-attendance v-model="editLessonAttendaceModalShow" ></edit-lesson-attendance>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Class from "../../../store/entities/class";
import Teacher from "../../../store/entities/teacher";
import FullCalendar from "@fullcalendar/vue";
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import { dateToLocalArray } from "@fullcalendar/core/datelib/marker";
import timeGridPlugin from "@fullcalendar/timegrid";
import PageRequest from "../../../store/entities/page-request";
import CreateLessonAttendance from "../../students/lessonAttendance/create-lesson-attendance.vue";
import EditLessonAttendance from "../../students/lessonAttendance/edit-lesson-attendance.vue";
class PageTeacherRequest extends PageRequest {
  teacherId?: number;
  start?: Date;
  end?: Date;
}
@Component({
  components: { FullCalendar, CreateLessonAttendance, EditLessonAttendance }
})
export default class ClassBusiness extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  teacher: Teacher = new Teacher();
  calenderShow: boolean = true;
  createLessonAttendaceModalShow = false;
  editLessonAttendaceModalShow = false;
  currentDate: Date = null;
  pagerequest: PageTeacherRequest = new PageTeacherRequest();
  calendarPlugins: any = [dayGridPlugin, interactionPlugin, timeGridPlugin];
  get locale() {
    return abp.localization.currentLanguage.name;
  }
  refetchEvents() {
    (this.$refs.calendar as any).getApi().refetchEvents();
  }
  async events(arg, callback) {
    await this.getpage(500, arg.start, arg.end);
    var list = this.$store.state.lesson.list.map(m => {
      let state = "未考勤";
      let color = new Date(m.endTime) < new Date() ? "#aaa" : "#0f0";
      if (m.isFinish) {
        color = "#57a3f3";
        state = "已考勤";
      }
      return {
        id: m.id,
        start: m.startTime,
        end: m.endTime,
        title: `${m.class.name}\n${m.subject}--${state}`,
        color: color,
        lesson: m
      };
    });
    callback(list);
  }
  eventClick(arg) {
    let lesson = arg.event.extendedProps.lesson;
    if (new Date(lesson.startTime) <= new Date()) {
      this.$store.commit("lesson/edit", lesson);
      if (lesson.isFinish) {
        this.editLessonAttendaceModalShow = true;
      } else {
        this.createLessonAttendaceModalShow = true;
      }
    }
  }

  get list() {
    return this.$store.state.lesson.list;
  }
  get loading() {
    return this.$store.state.lesson.loading;
  }

  async getpage(count = null, start = null, end = null) {
    this.pagerequest.teacherId = this.teacher.id;
    this.pagerequest.start = start;
    this.pagerequest.end = end;
    if (count === null) {
      this.pagerequest.maxResultCount = this.pageSize;
      this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;
    } else {
      this.pagerequest.maxResultCount = count;
    }

    await this.$store.dispatch({
      type: "lesson/getAll",
      data: this.pagerequest
    });
  }

  pageChange(page: number) {
    this.$store.commit("lesson/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("lesson/setPageSize", pagesize);
    this.getpage();
  }

  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.teacher = Util.extend(
        true,
        {},
        this.$store.state.teacher.editTeacher
      );
      this.calenderShow = true;
      setTimeout(() => (this.$refs.calendar as any).getApi().render(), 300);
    }
  }
  get pageSize() {
    return this.$store.state.lesson.pageSize;
  }
  get totalCount() {
    return this.$store.state.lesson.totalCount;
  }
  get currentPage() {
    return this.$store.state.lesson.currentPage;
  }
  columns = [
    // {
    //   title: this.L("Index"),
    //   key: "id",
    //   render: (h: any, params: any) => {
    //     return h("span", ("000000" + params.row.id).slice(-6));
    //   }
    // },

    {
      title: this.L("Subject"),
      key: "subject"
    },
    {
      title: this.L("ClassName"),
      key: "className",
      render: (h: any, params: any) => {
        return h("span", params.row.class.name);
      }
    },
    {
      title: this.L("LessonDate"),
      key: "lessonDate",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.lessonDate).toLocaleDateString());
      }
    },
    {
      title: this.L("TimePeriod"),
      key: "timePeriod",
      render: (h, params) => {
        return h(
          "span",
          params.row.start.slice(0, 5) + " - " + params.row.end.slice(0, 5)
        );
      }
    },
    {
      title: this.L("Teacher"),
      key: "teacher",
      render: (h: any, params: any) => {
        return h("span", params.row.teacher.name);
      }
    },
    {
      title: this.L("IsFinish"),
      key: "isFinish",
      render: (h: any, params: any) => {
        return h(
          "span",
          params.row.isFinish ? this.L("Fininshed") : this.L("Unfinished")
        );
      }
    }
  ];
}
</script>
<style lang='less'>
@import "~@fullcalendar/core/main.css";
@import "~@fullcalendar/daygrid/main.css";
@import "~@fullcalendar/timegrid/main.css";
</style>
