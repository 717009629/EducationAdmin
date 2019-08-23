<template>
  <div>
    <Modal :value="value" @on-visible-change="visibleChange" :mask-closable="false" width="1300px">
      <div slot="header">
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{L('Lesson')}}</span>
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{student.name}}</span>
        <Button @click="changeView" size='small'>{{calenderShow? L("List"):L("Calendar")}}</Button>
      </div>
      <div>
        <FullCalendar ref="calendar" v-show="calenderShow" defaultView="dayGridWeek" :plugins="calendarPlugins" :locale="locale" :events='events' :eventLimit='5'
                      :header="{left:'title',center:'',right:'dayGridWeek, dayGridMonth today prev,next'}" :showNonCurrentDates='true' :displayEventTime='false'
                      :buttonText="{today:L('Today'),month:L('Month'),week:L('Week'),}"></FullCalendar>

        <!-- <Card dis-hover> -->
        <div v-if="!calenderShow">
          <Form ref="queryForm" :label-width="100" label-position="left" inline>

            <Row>
              <Button @click="create" icon="android-add" type="primary" v-if="hasPermission('Pages.Lessons.Create')">{{L('Add')}}</Button>
            </Row>
          </Form>
          <div class="margin-top-10">
            <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
              <!-- <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Lessons.Edit')">
                <Button v-if="hasPermission('Pages.Lessons.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              </template> -->
            </Table>
            <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
            </Page>
          </div>

        </div>
      </div>
      <div slot="footer"></div>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "../../../store/entities/student";
// import CreateLesson from "../lesson/create-lesson.vue";
// import EditLesson from "../lesson/edit-lesson.vue";
import FullCalendar from "@fullcalendar/vue";
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import { dateToLocalArray } from "@fullcalendar/core/datelib/marker";
import PageRequest from "../../../store/entities/page-request";
class PageStudentRequest extends PageRequest {
  studentId?: number;
  start?: Date;
  end?: Date;
}

@Component({ components: { FullCalendar } })
export default class StudentBusiness extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  student: Student = new Student();
  createModalShow: boolean = false;
  editModalShow: boolean = false;
  calenderShow: boolean = true;
  currentDate: Date = null;
  pagerequest: PageStudentRequest = new PageStudentRequest();
  start: Date;
  end: Date;

  get locale() {
    return abp.localization.currentLanguage.name;
  }
  async changeView() {
    if (this.calenderShow) {
      await this.getpage();
    }

    this.calenderShow = !this.calenderShow;
  }
  getCalendarPage() {
    (this.$refs.calendar as any).getApi().refetchEvents();
  }
  async events(arg, callback) {
    this.start = arg.start;
    this.end = arg.end;
    await this.getpage(500, arg.start, arg.end);

    var list = this.$store.state.lesson.list.map(m => {
      return {
        id: m.id,
        start: new Date(m.lessonDate).setHours(m.lessonNumber + 8),
        title: `#${m.lessonNumber}-${m.class.name}-${m.course}-${m.teacher.name}`,
        color:
          new Date(new Date(m.lessonDate).toLocaleDateString()) < new Date()
            ? "#aaa"
            : "#0f0",
        lesson: m
      };
    });
    callback(list);
    return list;
  }

  calendarPlugins: any = [dayGridPlugin, interactionPlugin];

  get list() {
    return this.$store.state.lesson.list;
  }
  get loading() {
    return this.$store.state.lesson.loading;
  }
  create() {
    this.createModalShow = true;
  }
  // edit(row) {
  //   this.$store.commit("lesson/edit", row);
  //   this.editModalShow = true;
  // }
  pageChange(page: number) {
    this.$store.commit("lesson/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("lesson/setPageSize", pagesize);
    this.getpage();
  }
  // dateClick(arg) {
  //   this.currentDate = arg.date;
  //   this.createModalShow = true;
  // }
  // eventClick(arg) {
  //   this.$store.commit("lesson/edit", arg.event.extendedProps.lesson);
  //   this.editModalShow = true;
  // }

  async getpage(count = null, start = null, end = null) {
    this.pagerequest.studentId = this.student.id;
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
  get pageSize() {
    return this.$store.state.lesson.pageSize;
  }
  get totalCount() {
    return this.$store.state.lesson.totalCount;
  }
  get currentPage() {
    return this.$store.state.lesson.currentPage;
  }

  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.getCalendarPage();
      this.student = Util.extend(
        true,
        {},
        this.$store.state.student.editStudent
      );
      this.calenderShow = true;
      
    }
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
      title: this.L("Course"),
      key: "course"
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
      title: this.L("LessonNumber"),
      key: "lessonNumber"
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
    // {
    //   title: this.L("Actions"),
    //   key: "Actions",
    //   width: 150,
    //   slot: "action"
    // }
  ];
}
</script>
<style lang='less'>
@import "~@fullcalendar/core/main.css";
@import "~@fullcalendar/daygrid/main.css";
</style>
