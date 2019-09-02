<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row>
            <Button v-if="hasPermission('Pages.Options.Create')" @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Refresh')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Tabs v-model="category">
            <TabPane v-for="category in categories" :label="L(category.name)" :name="category.value" :key="category.value">
              <!-- <option-tab :category="category.value"></option-tab> -->
              <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list.filter(m=>m.category===category.value)">
                <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Options.Edit')">
                  <Button v-if="hasPermission('Pages.Options.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
                </template>
              </Table>
            </TabPane>
          </Tabs>
          <!-- <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Options.Edit')">
              <Button v-if="hasPermission('Pages.Options.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page> -->
        </div>
      </div>
    </Card>
    <create-option v-model="createModalShow" @save-success="getpage" :category='category'></create-option>
    <edit-option v-model="editModalShow" @save-success="getpage"></edit-option>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateOption from "./create-option.vue";
import EditOption from "./edit-option.vue";
import Categories from "../../../store/entities/optionCategory";

class PageOptionRequest extends PageRequest {}

@Component({
  components: { CreateOption, EditOption }
})
export default class Options extends AbpBase {
  pagerequest: PageOptionRequest = new PageOptionRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  categories = Categories;
  category: string = this.categories[0].value;
  get list() {
    return this.$store.state.option.list;
  }
  get loading() {
    return this.$store.state.option.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("option/edit", row);
    this.editModalShow = true;
  }

  async getpage() {
    await this.$store.dispatch({
      type: "option/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.option.pageSize;
  }
  get totalCount() {
    return this.$store.state.option.totalCount;
  }
  get currentPage() {
    return this.$store.state.option.currentPage;
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
      title: this.L("OptionName"),
      key: "name"
    },
    {
      title: this.L("IsActive"),
      render: (h: any, params: any) => {
        return h("span", params.row.isActive ? this.L("Yes") : this.L("No"));
      }
    },
    {
      title: this.L("Actions"),
      key: "Actions",
      width: 250,
      slot: "action"
    }
  ];
  async created() {
    this.getpage();
  }
}
</script>