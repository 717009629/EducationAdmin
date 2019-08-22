<template>
  <div>
    <Modal :title="L('Student')" :value="value" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false" :width='1000'>
      <!-- <Card dis-hover> -->
      <div class="margin-top-10">
        <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list" @on-selection-change='selectionChange'>

        </Table>
      </div>

    </Modal>

    <!-- </Card> -->

  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Class from "../../../store/entities/class";
import Order from "../../../store/entities/order";
@Component({
  components: {}
})
export default class ClassOrder extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  clas: Class = new Class();
  createModalShow: boolean = false;
  editModalShow: boolean = false;
  contractModalShow: boolean = false;
  selectionList=[];
  get list() {
    return this.$store.state.order.freeList;
  }
  get loading() {
    return this.$store.state.order.loading;
  }

  selectionChange(selection){
    this.selectionList=selection;
    console.log(this.selectionList);
  }

  async getpage() {
    await this.$store.dispatch({
      type: "order/getAllFree",
      data: { courseId: this.clas.courseId }
    });
  }

  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.clas = Util.extend(true, {}, this.$store.state.class.editClass);
      this.getpage();
    }
  }

  columns = [
    {
      type: "selection",
      width: 60,
      align: "center"
    },
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
    }
  ];
}
</script>

