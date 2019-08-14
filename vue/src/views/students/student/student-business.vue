<template>
  <div>
    <Modal :title="L('Business')" :value="value" @on-visible-change="visibleChange" :mask-closable="false" width="1200px">
      <div>
        <!-- <Card dis-hover> -->

        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <i-col span="3">
            <h3>{{L('StudentName')}}:{{student.name}}</h3>
            </i-col>
            <i-col span="5">
            <h3>{{L('Phone')}}:{{student.phone}}</h3>
            </i-col>
            <i-col span="3">
            <h3>{{L('Father')}}:{{student.father}}</h3>
            </i-col>
            <i-col span="5">
            <h3>{{L('FatherPhone')}}:{{student.fatherPhone}}</h3>
            </i-col>
            <i-col span="3">
            <h3>{{L('Mother')}}:{{student.mother}}</h3>
            </i-col>
            <i-col span="5">
            <h3>{{L('MotherPhone')}}:{{student.motherPhone}}</h3>
            </i-col>
          </Row>
          <!-- <hr
            style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0"
          />-->
        </Form>
        <div class="margin-top-10">
          <Tabs type="card" v-model="selecteTab">
            <TabPane :label="L('Order')" name="order" v-if="hasPermission('Pages.Records')">
              <student-order :studentId="student.id" @goContract="selecteTab='contract'"></student-order>
            </TabPane>
            <TabPane :label="L('Contract')" name="contract" v-if="hasPermission('Pages.Records')">
              <student-contract :studentId="student.id"></student-contract>
            </TabPane>
          </Tabs>
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
import Student from "../../../store/entities/student";
import StudentOrder from "./student-order.vue";
import StudentContract from "./student-contract.vue";

@Component({ components: { StudentOrder, StudentContract } })
export default class StudentBusiness extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  student: Student = new Student();
  selecteTab: String = "order";

  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.selecteTab = "order";
      this.student = Util.extend(
        true,
        {},
        this.$store.state.student.editStudent
      );
    }
  }
}
</script>

