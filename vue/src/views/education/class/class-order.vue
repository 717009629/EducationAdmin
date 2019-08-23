<template>
  <div>
    <Modal :title="L('Student')" :value="value" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false" :width='1000'>
      <Form ref="queryForm" :label-width="100" label-position="left" v-if="clas.course&&clas.course.classType===0" inline>
        <!-- <hr style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0" /> -->
        <Row>
          <Button @click="addOrder" icon="android-add" type="primary" v-if="hasPermission('Pages.Orders.Create')">{{L('Add')}}</Button>
        </Row>
      </Form>
      <!-- <Card dis-hover> -->
      <div class="margin-top-10">
        <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
          <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Orders.Edit')">
            <!-- <Button v-if="hasPermission('Pages.Orders.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button> -->
          </template>
        </Table>
      </div>
      <div slot="footer"></div>
      <class-add-order v-model="addOrderModalShow" @save-success="getpage"></class-add-order>
      <!-- <edit-order v-model="editModalShow" @save-success="getpage"></edit-order> -->
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
import ClassAddOrder from "./class-add-order.vue";
// import EditOrder from "../../sales/order/edit-order.vue";
@Component({
  components: { ClassAddOrder }
})
export default class ClassOrder extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  clas: Class = new Class();
  addOrderModalShow: boolean = false;
  editModalShow: boolean = false;
  contractModalShow: boolean = false;
  get list() {
    return this.$store.state.order.list;
  }
  get loading() {
    return this.$store.state.order.loading;
  }
  addOrder() {
    this.addOrderModalShow = true;
  }
  // edit(row) {
  //   this.$store.commit("order/edit", row);
  //   this.editModalShow = true;
  // }

  async getpage() {
    await this.$store.dispatch({
      type: "order/getAll",
      data: { classId: this.clas.id }
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
      title: this.L("Actions"),
      key: "Actions",
      width: 220,
      slot: "action"
    }
  ];
}
</script>

