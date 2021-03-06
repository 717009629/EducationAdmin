<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="80" label-position="left" inline>
          <Row :gutter="16">
            <i-col span="6">
              <FormItem :label="L('Keyword')+':'" style="width:100%">
                <Input v-model="pagerequest.keyword" :placeholder="L('UserName')+'/'+L('Name')" />
              </FormItem>
            </i-col>
            <i-col span="6">
              <FormItem :label="L('IsActive')+':'" style="width:100%">
                <!--Select should not set :value="'All'" it may not trigger on-change when first select 'NoActive'(or 'Actived') then select 'All'-->
                <Select :placeholder="L('Select')" @on-change="isActiveChange">
                  <Option value="All">{{L('All')}}</Option>
                  <Option value="Actived">{{L('Actived')}}</Option>
                  <Option value="NoActive">{{L('NoActive')}}</Option>
                </Select>
              </FormItem>
            </i-col>
            <i-col span="6">
              <FormItem :label="L('CreationTime')+':'" style="width:100%">
                <DatePicker v-model="creationTime" type="datetimerange" format="yyyy-MM-dd" style="width:100%" placement="bottom-end" :placeholder="L('SelectDate')"></DatePicker>
              </FormItem>
            </i-col>
          </Row>
          <Row>
            <Button @click="create" icon="android-add" type="primary" size="large" v-if="hasPermission('Pages.Users.Create')">{{L('Add')}}</Button>
            <Button icon="ios-search" type="primary" size="large" @click="pageChange(1)" class="toolbar-btn">{{L('Find')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Users.Edit')||hasPermission('Pages.Users.Delete')">
              <Button v-if="hasPermission('Pages.Users.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              <Button v-if="hasPermission('Pages.Users.Delete')" type="primary" size="small" @click="remove(row)" style="margin-right:5px">{{L('Delete')}}</Button>
               <Button v-if="hasPermission('Pages.Users.ChangePassword')" type="primary" size="small" @click="changePassword(row)" style="margin-right:5px">{{L('ChangePassword')}}</Button>
            </template>

          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <create-user v-model="createModalShow" @save-success="getpage"></create-user>
    <edit-user v-model="editModalShow" @save-success="getpage"></edit-user>
     <change-password v-model="changePasswordModalShow"></change-password>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateUser from "./create-user.vue";
import EditUser from "./edit-user.vue";
import ChangePassword from "./change-password.vue";
class PageUserRequest extends PageRequest {
  keyword: string;
  isActive: boolean = null; //nullable
  from: Date;
  to: Date;
}

@Component({
  components: { CreateUser, EditUser, ChangePassword }
})
export default class Users extends AbpBase {
  changePassword(row) {
    this.$store.commit("user/edit", row);
    this.changePasswordModalShow = true;
  }
  edit(row) {
    this.$store.commit("user/edit", row);
    this.editModalShow = true;
  }
  async remove(row) {
    this.$Modal.confirm({
      title: this.L("Tips"),
      content: this.L("DeleteUserConfirm"),
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
  //filters
  pagerequest: PageUserRequest = new PageUserRequest();
  creationTime: Date[] = [];

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  changePasswordModalShow: boolean = false;
  get list() {
    return this.$store.state.user.list;
  }
  get loading() {
    return this.$store.state.user.loading;
  }
  create() {
    this.createModalShow = true;
  }
  isActiveChange(val: string) {
    console.log(val);
    if (val === "Actived") {
      this.pagerequest.isActive = true;
    } else if (val === "NoActive") {
      this.pagerequest.isActive = false;
    } else {
      this.pagerequest.isActive = null;
    }
  }
  pageChange(page: number) {
    this.$store.commit("user/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("user/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;
    //filters

    if (this.creationTime.length > 0) {
      this.pagerequest.from = this.creationTime[0];
    }
    if (this.creationTime.length > 1) {
      this.pagerequest.to = this.creationTime[1];
    }

    await this.$store.dispatch({
      type: "user/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.user.pageSize;
  }
  get totalCount() {
    return this.$store.state.user.totalCount;
  }
  get currentPage() {
    return this.$store.state.user.currentPage;
  }
  columns = [
    {
      title: this.L("UserName"),
      key: "userName"
    },
    {
      title: this.L("Name"),
      key: "name"
    },
    {
      title: this.L("Sex"),
      render: (h: any, params: any) => {
        return h(
          "span",
          params.row.sex
            ? this.L("Male")
            : params.row.sex === false
            ? this.L("Female")
            : ""
        );
      }
    },
    {
      title: this.L("UserType"),
      key: "type",
      render: (h, params) => {
        let t = params.row.type;
        return h(
          "span",
          t === 0
            ? this.L("Business")
            : t === 1
            ? this.L("Teacher")
            : t === 2
            ? this.L("Admin")
            : ""
        );
      }
    },
    {
      title: this.L("IsActive"),
      render: (h: any, params: any) => {
        return h("span", params.row.isActive ? this.L("Yes") : this.L("No"));
      }
    },
    {
      title: this.L("CreationTime"),
      key: "creationTime",
      render: (h: any, params: any) => {
        return h(
          "span",
          new Date(params.row.creationTime).toLocaleDateString()
        );
      }
    },
    {
      title: this.L("LastLoginTime"),
      render: (h: any, params: any) => {
        return h("span",params.row.lastLoginTime?new Date(params.row.lastLoginTime).toLocaleString():'');
      }
    },
    {
      title: this.L("Actions"),
      key: "Actions",
      width: 200,
      slot: "action"
    }
  ];
  async created() {
    this.getpage();
    await this.$store.dispatch({
      type: "user/getRoles"
    });
  }
}
</script>