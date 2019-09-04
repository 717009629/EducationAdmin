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
          <!-- <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Orders.Edit')">
            <Button v-if="hasPermission('Pages.Orders.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
          </template> -->
        </Table>
      </div>
      <div slot="footer">
        <Button @click="cancel">{{L('Cancel')}}</Button>
        <Button @click="save" type="primary">{{L('OK')}}</Button>
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
export default class CreateLessonAttendance extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  lesson: Lesson = new Lesson();
  selectItems = [];
  get list() {
    return this.$store.state.order.list;
  }
  get loading() {
    return this.$store.state.order.loading;
  }


  selectChange(selection) {
    this.selectItems = selection;
  }

  async getpage() {
    await this.$store.dispatch({
      type: "order/getAll",
      data: { classId: this.lesson.classId }
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
  cancel() {
    this.$emit("input", false);
  }
  save() {
    let content = `${this.L("Total")} : ${this.list.length} ; ${this.L(
      "Attended"
    )} : ${this.selectItems.length} . ${this.L("Are you sure to save")} ?`;

    var orderIds = this.selectItems.map(m => m.id);
    this.$Modal.confirm({
      title: this.L("Tips"),
      content: content,
      okText: this.L("Yes"),
      cancelText: this.L("No"),
      onOk: () => {
        //this.$Modal.remove();
        setTimeout(() => {
          this.$store.dispatch({
            type: "lessonAttendance/createMult",
            data: { lessonId: this.lesson.id, orderIds: orderIds }
          });
          this.$emit("input", false);
        }, 300);
      }
    });
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
        return h("span", ("000000" + params.row.studentId).slice(-6));
      }
    },
    {
      title: this.L("StudentName"),
      key: "name",
      render: (h: any, params: any) => {
        return h("span", params.row.student.name);
      }
    },
    {
      title: this.L("Phone"),
      key: "name",
      render: (h: any, params: any) => {
        return h("span", params.row.student.phone);
      }
    },
    {
      title: this.L("Attended"),
      type: "selection",
      width: 60,
      align: "center"
    }
  ];
}
</script>
