<template>
  <div>
    <Modal :value="value" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false" :width='1000'>
      <div slot="header">
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{L('LessionAttendance')}}</span>
        <span style="line-height:20px; font-size:14px;color:#17233d;font-weight:bold;margin-right:20px">{{order.student?order.student.name:''}}</span>
      </div>
      <!-- <Card dis-hover> -->
      <div class="margin-top-10">
        <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list" @on-selection-change='selectChange' :row-class-name="rowClassName">
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
  order: Order = new Order();

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
      data: { orderId: this.order.id,maxResultCount:1000 }
    });
  }
  rowClassName(row, index) {
    if (!row.attended) {
      return "error";
    }
    return "";
  }

  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.order = Util.extend(true, {}, this.$store.state.order.editOrder);
      this.getpage();
    }
  }
  columns = [
    {
      title: this.L("Index"),
      key: "id",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.id).slice(-6));
      }
    },
    {
      title: this.L("LessonIndex"),
      key: "LessonId",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.lesson.id).slice(-6));
      }
    },
    {
      title: this.L("ClassName"),
      key: "className",
      render: (h: any, params: any) => {
        return h("span", params.row.lesson.class.name);
      }
    },
    {
      title: this.L("LessonDate"),
      key: "lessonDate",
      render: (h: any, params: any) => {
        return h(
          "span",
          new Date(params.row.lesson.lessonDate).toLocaleDateString()
        );
      }
    },
    {
      title: this.L("TimePeriod"),
      key: "timePeriod",
      render: (h, params) => {
        return h(
          "span",
          params.row.lesson.start.slice(0, 5) +
            " - " +
            params.row.lesson.end.slice(0, 5)
        );
      }
    },

    {
      title: this.L("Teacher"),
      key: "teacher",
      render: (h: any, params: any) => {
        return h("span", params.row.lesson.teacher.name);
      }
    },
    {
      title: this.L("Attended"),
      key: "attended",
      render: (h: any, params: any) => {
        return h("span", params.row.attended ? this.L("Yes") : this.L("No"));
      }
    }
  ];
}
</script>
