<template>
  <div>
    <Card dis-hover>
      <FullCalendar v-if="isTenant" ref='calendar' defaultView="timeGridWeek" :plugins="calendarPlugins" :locale="locale" :events='events' :displayEventTime='true' :eventLimit='true' @eventClick='eventClick'
                    :allDaySlot='false' minTime='07:00:00' maxTime='21:00:00' slotDuration='00:15:00' slotLabelInterval='01:00' height='auto'
                    :header="{left:'title',center:'',right:'timeGridWeek, dayGridMonth today prev,next'}" :buttonText="{today:L('Today'),month:L('Month'),week:L('Week'),}">
      </FullCalendar>
    </Card>

    <create-lesson-attendance v-model="createLessonAttendaceModalShow" @save-success="refetchEvents"></create-lesson-attendance>
    <edit-lesson-attendance v-model="editLessonAttendaceModalShow"></edit-lesson-attendance>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../lib/util";
import AbpBase from "../../lib/abpbase";
import FullCalendar from "@fullcalendar/vue";
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import { dateToLocalArray } from "@fullcalendar/core/datelib/marker";
import timeGridPlugin from "@fullcalendar/timegrid";
import CreateLessonAttendance from "../students/lessonAttendance/create-lesson-attendance.vue";
import EditLessonAttendance from "../students/lessonAttendance/edit-lesson-attendance.vue";

@Component({
  components: { FullCalendar, CreateLessonAttendance, EditLessonAttendance }
})
export default class Home extends AbpBase {
  createLessonAttendaceModalShow = false;
  editLessonAttendaceModalShow = false;
  calendarPlugins: any = [dayGridPlugin, interactionPlugin, timeGridPlugin];
  get currentUser() {
    return this.$store.state.session.user;
  }
  get isTenant() {
    return !!abp.session.tenantId;
  }

  get locale() {
    return abp.localization.currentLanguage.name;
  }
  refetchEvents() {
    (this.$refs.calendar as any).getApi().refetchEvents();
  }
  async events(arg, callback) {
    await this.getLesson(500, arg.start, arg.end);
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

  async getLesson(count, start = null, end = null) {
    while (!this.currentUser) {
      await new Promise(resolve => setTimeout(resolve, 100));
    }
    if (this.currentUser.type === 1) {
      await this.$store.dispatch({
        type: "lesson/getAll",
        data: {
          maxResultCount: count,
          teacherId: this.currentUser.id,
          start,
          end
        }
      });
    }
  }

  async created() {}
}
</script>
<style scoped>
@import "~@fullcalendar/core/main.css";
@import "~@fullcalendar/daygrid/main.css";
@import "~@fullcalendar/timegrid/main.css";
</style>

