<template>
  <div>
    <Modal :value="value" @on-visible-change="visibleChange" :mask-closable="false" width="1100px">
      <div slot="header">
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{L('Lesson')}}</span>
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{student.name}}</span>
        <Button @click="calenderShow=!calenderShow" size='small'>{{calenderShow? L("List"):L("Calendar")}}</Button>
      </div>
      <div>
        <FullCalendar v-if="calenderShow" defaultView="dayGridMonth" :plugins="calendarPlugins" :locale="locale" :events='events' @dateClick='dateClick' @eventClick='eventClick'
                      :displayEventTime='true' :buttonText="{today:L('Today')}"></FullCalendar>

        <!-- <Card dis-hover> -->
        <div v-if="!calenderShow">
          <Form ref="queryForm" :label-width="100" label-position="left" inline>

            <Row>
              <Button @click="create" icon="android-add" type="primary" v-if="hasPermission('Pages.Lessons.Create')">{{L('Add')}}</Button>
            </Row>
          </Form>
          <div class="margin-top-10">
            <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
              <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Lessons.Edit')">
                <Button v-if="hasPermission('Pages.Lessons.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              </template>
            </Table>
          </div>

        </div>
      </div>
      <div slot="footer"></div>
      <create-lesson v-model="createModalShow" @save-success="getpage" :date='currentDate'></create-lesson>
      <edit-lesson v-model="editModalShow" @save-success="getpage"></edit-lesson>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "../../../store/entities/student";
import CreateLesson from "../lesson/create-lesson.vue";
import EditLesson from "../lesson/edit-lesson.vue";
import FullCalendar from "@fullcalendar/vue";
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import { dateToLocalArray } from "@fullcalendar/core/datelib/marker";

@Component({ components: { CreateLesson, EditLesson, FullCalendar } })
export default class StudentBusiness extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  student: Student = new Student();
  createModalShow: boolean = false;
  editModalShow: boolean = false;
  calenderShow: boolean = false;
  currentDate: Date = null;
  get locale(){
    return abp.localization.currentLanguage.name
  }
  get events() {
    var list = this.$store.state.lesson.list.map(m => {
      return {
        id: m.id,
        start: m.lessonDate,
        title: m.course + " " + m.teacher.name,
        color:
          new Date(new Date(m.lessonDate).toLocaleDateString()) < new Date()
            ? "#aaa"
            : "#0f0",
        lesson: m
      };
    });
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
  edit(row) {
    this.$store.commit("lesson/edit", row);
    this.editModalShow = true;
  }
  dateClick(arg) {
    this.currentDate = arg.date;
    this.createModalShow = true;
  }
  eventClick(arg) {
    this.$store.commit("lesson/edit", arg.event.extendedProps.lesson);
    this.editModalShow = true;
  }

  async getpage() {
    await this.$store.dispatch({
      type: "lesson/getAll",
      data: { studentId: this.student.id }
    });
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
      this.calenderShow = false;
    } else {
      this.student = Util.extend(
        true,
        {},
        this.$store.state.student.editStudent
      );
      this.getpage();
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
      title: this.L("StudentName"),
      key: "studentName",
      render: (h: any, params: any) => {
        return h("span", params.row.student.name);
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
    },
    {
      title: this.L("Actions"),
      key: "Actions",
      width: 150,
      slot: "action"
    }
  ];
}
</script>
<style lang='less'>
@import "~@fullcalendar/core/main.css";
@import "~@fullcalendar/daygrid/main.css";
</style>
