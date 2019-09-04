<template>
  <div>
    <Modal :value="value" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false" :width='1000'>
      <div slot="header">
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{L('LessionAttendance')}}</span>
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{lesson.class?lesson.class.name:''}}</span>
      </div>
      <!-- <Card dis-hover> -->
      <div class="margin-top-10">
        <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list" @on-selection-change='selectChange'>
          <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Orders.Edit')">
            <!-- <Button v-if="hasPermission('Pages.Orders.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button> -->
          </template>
        </Table>
      </div>
      <div slot="footer">
      </div>
      <!-- <class-add-order v-model="addOrderModalShow" @save-success="getpage"></class-add-order> -->
      <!-- <edit-order v-model="editModalShow" @save-success="getpage"></edit-order> -->
    </Modal>

    <!-- </Card> -->

  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Lesson from "../../../store/entities/lesson";
import Order from "../../../store/entities/order";
//import ClassAddOrder from "./class-add-order.vue";
// import EditOrder from "../../sales/order/edit-order.vue";
@Component({
  components: {}
})
export default class EditLessonAttendance extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  lesson: Lesson = new Lesson();

  selectItems = [];
  get list() {
    return this.$store.state.lessonAttendance.list;
  }
  get loading() {
    return this.$store.state.order.loading;
  }
  // edit(row) {
  //   this.$store.commit("order/edit", row);
  //   this.editModalShow = true;
  // }
  selectChange(selection) {
    this.selectItems = selection;
  }

  async getpage() {
    await this.$store.dispatch({
      type: "lessonAttendance/getAll",
      data: { lessonId: this.lesson.id }
    });
  }

  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.lesson = Util.extend(true, {}, this.$store.state.lesson.editLesson);
      this.getpage();
    }
  }
  columns = [
    {
      title: this.L("OrderIndex"),
      key: "name",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.id).slice(-6));
      }
    },
    {
      title: this.L("StudentIndex"),
      key: "name",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.order.studentId).slice(-6));
      }
    },
    {
      title: this.L("StudentName"),
      key: "name",
      render: (h: any, params: any) => {
        return h("span", params.row.order.student.name);
      }
    },
    {
      title: this.L("Phone"),
      key: "name",
      render: (h: any, params: any) => {
        return h("span", params.row.order.student.phone);
      }
    },
    {
      title: this.L("Attended"),
      key: "attended",
      render: (h: any, params: any) => {
        return h("span", params.row.attended ? this.L("Yes") : this.L("No"));
      }
    },
    {
      title: this.L("Actions"),
      key: "Actions",
      slot: "action"
    }
  ];
}
</script>
