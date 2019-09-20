<template>
  <div>
    <Modal :title="L('Record')" :value="value" @on-visible-change="visibleChange" :mask-closable="false" width="1200px">
      <div>
        <!-- <Card dis-hover> -->

        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <i-col span="4">
              <h3>{{L('StudentName')}}:{{customer.studentName}}</h3>
            </i-col>
            <i-col span="4">
              <h3>{{L('Father')}}:{{customer.father}}</h3>
            </i-col>
            <i-col span="5">
              <h3>{{L('FatherPhone')}}:{{customer.fatherPhone}}</h3>
            </i-col>
            <i-col span="4">
              <h3>{{L('Mother')}}:{{customer.mother}}</h3>
            </i-col>
            <i-col span="5">
              <h3>{{L('MotherPhone')}}:{{customer.motherPhone}}</h3>
            </i-col>
          </Row>
          <hr style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0" />
        </Form>
        <div>
          <!-- <Card dis-hover> -->

          <Form ref="queryForm" :label-width="100" label-position="left" inline>
            <!-- <hr style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0" /> -->
            <Row>
              <Button @click="create" icon="android-add" type="primary" v-if="hasPermission('Pages.Records.Create')">{{L('Add')}}</Button>
            </Row>
          </Form>
          <div class="margin-top-10">
            <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
              <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Records.Edit')">
                <Button v-if="hasPermission('Pages.Records.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              </template>
            </Table>
          </div>
          <!-- </Card> -->
          <create-record v-model="createModalShow" @save-success="getpage"></create-record>
          <edit-record v-model="editModalShow" @save-success="getpage"></edit-record>
        </div>
        <!-- </Card> -->
      </div>
      <div slot="footer"></div>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Customer from "../../../store/entities/customer";
import CreateRecord from "../../sales/record/create-record.vue";
import EditRecord from "../../sales/record/edit-record.vue";

@Component({ components: { CreateRecord, EditRecord } })
export default class CustomerBusiness extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  customer: Customer = new Customer();
  createModalShow: boolean = false;
  editModalShow: boolean = false;

  get list() {
    return this.$store.state.record.list;
  }
  get loading() {
    return this.$store.state.record.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("record/edit", row);
    this.editModalShow = true;
  }

  async getpage() {
    await this.$store.dispatch({
      type: "record/getAll",
      data: { customerId: this.customer.id }
    });
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.customer = Util.extend(
        true,
        {},
        this.$store.state.customer.editCustomer
      );
       this.getpage();
    }
  }
  columns = [
    {
      title: this.L("RecordContent"),
      key: "content"
    },
    {
      title: this.L("State"),
      key: "state"
    },
    {
      title: this.L("RecordDate"),
      key: "date",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.date).toLocaleDateString());
      }
    },
    {
      title: this.L("SalesmanName"),
      key: "salesmanName"
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

