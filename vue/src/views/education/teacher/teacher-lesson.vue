<template>
  <div>
    <Modal :value="value" @on-visible-change="visibleChange" :mask-closable="false" width="1100px">
      <div slot="header">
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{L('Lesson')}}</span>
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{teacher.name}}</span>
        <Button @click="calenderShow=!calenderShow" size='small'>{{calenderShow? L("List"):L("Calendar")}}</Button>
      </div>
      <div>
        <FullCalendar v-if="calenderShow" defaultView="dayGridMonth" :plugins="calendarPlugins" :locale="locale" :events='events' 
                      :displayEventTime='false' :buttonText="{today:L('Today')}"></FullCalendar>

        <!-- <Card dis-hover> -->
        <div v-if="!calenderShow">
          <Form ref="queryForm" :label-width="100" label-position="left" inline>

          </Form>
          <div class="margin-top-10">
            <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
              
            </Table>
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
import Teacher from "../../../store/entities/teacher";
import FullCalendar from "@fullcalendar/vue";
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import { dateToLocalArray } from "@fullcalendar/core/datelib/marker";

@Component({ components: {  FullCalendar } })
export default class StudentBusiness extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  teacher: Teacher = new Teacher();
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
        title: m.course + " " + m.student.name,
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



  async getpage() {
    await this.$store.dispatch({
      type: "lesson/getAll",
      data: { teacherId: this.teacher.id }
    });
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
      this.calenderShow = false;
    } else {
      this.teacher = Util.extend(
        true,
        {},
        this.$store.state.teacher.editTeacher
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
    }
  ];
}
</script>
<style lang='less'>
@import "~@fullcalendar/core/main.css";
@import "~@fullcalendar/daygrid/main.css";
</style>
