<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="90" label-position="left" inline>
          <Row :gutter="16">
            <Col span="8">
            <FormItem :label="L('Keyword')+':'" style="width:100%">
              <Input v-model="pagerequest.keyword" :placeholder="L('RoleName')+'/'+L('DisplayName')+'/'+L('Description')"></Input>
            </FormItem>
            </Col>
          </Row>
          <Row>
            <Button @click="create" icon="android-add" type="primary" size="large" v-if="hasPermission('Pages.Roles.Create')">{{L('Add')}}</Button>
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Find')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Roles.Edit')||hasPermission('Pages.Roles.Delete')">
              <Button v-if="hasPermission('Pages.Roles.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              <Button v-if="hasPermission('Pages.Roles.Delete')" type="primary" size="small" @click="remove(row)" style="margin-right:5px">{{L('Delete')}}</Button>
            </template>

          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <create-role v-model="createModalShow" @save-success="getpage"></create-role>
    <edit-role v-model="editModalShow" @save-success="getpage"></edit-role>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateRole from "./create-role.vue";
import EditRole from "./edit-role.vue";

class PageRoleRequest extends PageRequest {
  keyword: string = "";
}

@Component({
  components: { CreateRole, EditRole }
})
export default class Roles extends AbpBase {
  edit(row) {
    this.$store.commit("role/edit", row);
    this.editModalShow = true;
  }

  pagerequest: PageRoleRequest = new PageRoleRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.role.list;
  }
  get loading() {
    return this.$store.state.role.loading;
  }
  create() {
    this.createModalShow = true;
  }
  async remove(row) {
    this.$Modal.confirm({
      title: this.L("Tips"),
      content: this.L("DeleteRolesConfirm"),
      okText: this.L("Yes"),
      cancelText: this.L("No"),
      onOk: async () => {
        await this.$store.dispatch({
          type: "role/delete",
          data: row
        });
        await this.getpage();
      }
    });
  }
  pageChange(page: number) {
    this.$store.commit("role/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("role/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "role/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.role.pageSize;
  }
  get totalCount() {
    return this.$store.state.role.totalCount;
  }
  get currentPage() {
    return this.$store.state.role.currentPage;
  }
  columns = [
    {
      title: this.L("RoleName"),
      key: "name"
    },
    {
      title: this.L("DisplayName"),
      key: "displayName"
    },
    {
      title: this.L("Description"),
      key: "description"
    },
    {
      title: this.L("IsStatic"),
      render: (h: any, params: any) => {
        return h("span", params.row.isStatic ? this.L("Yes") : this.L("No"));
      }
    },
    {
      title: this.L("Actions"),
      width: 150,
      slot: "action"
    }
  ];
  async created() {
    this.getpage();
    await this.$store.dispatch({
      type: "role/getAllPermissions"
    });
  }
}
</script>