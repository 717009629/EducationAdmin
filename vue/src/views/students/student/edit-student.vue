<template>
  <div>
    <Modal :title="L('EditStudent')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :width='800'>
      <Form ref="studentForm" label-position="top" :rules="studentRule" :model="student">
        <Tabs value="detail">
          <TabPane :label="L('BasicInfo')" name="detail">
            <Row :gutter='16'>
              <i-col span=12>
                <FormItem :label="L('StudentName')" prop="name">
                  <Input v-model="student.name" :maxlength="32" :minlength="2" />
                </FormItem>
              </i-col>
              <i-col span=12>
                <FormItem :label="L('Phone')" prop="phone">
                  <Input v-model="student.phone" />
                </FormItem>
              </i-col>

            </Row>
            <Row :gutter=16>
              <i-col span=12>
                <FormItem :label="L('Sex')" prop="sex">
                  <RadioGroup v-model="sex" type="button" style="width:100%">
                    <Radio label="male">{{L('Male')}}</Radio>
                    <Radio label="female">{{L('Female')}}</Radio>
                  </RadioGroup>
                </FormItem>
              </i-col>
              <i-col span=12>
                <FormItem :label="L('Birthday')" prop="birthday">
                  <DatePicker type="date" placeholder="Select date" v-model="student.birthday" style="width:100%"></DatePicker>
                </FormItem>
              </i-col>

            </Row>

            <Row :gutter='16'>
              <i-col span=8>
                <FormItem :label="L('Province')" prop="province">
                  <Select v-model="student.province">
                    <Option v-for="p in provinces" :key="p.name" :value="p.name" :label="p.name" :transfer="true"></Option>
                  </Select>
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('City')" prop="city">
                  <Select v-model="student.city">
                    <Option v-for="c in cities" :key="c.name" :value="c.name" :label="c.name" :transfer="true"></Option>
                  </Select>
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('District')" prop="district">
                  <Select v-model="student.district">
                    <Option v-for="d in districts" :key="d.name" :value="d.name" :label="d.name" :transfer="true"></Option>
                  </Select>
                </FormItem>
              </i-col>
            </Row>
            <FormItem :label="L('Address')" prop="address">
              <Input v-model="student.address" />
            </FormItem>
            <Row :gutter='16'>
              <i-col span=10>
                <FormItem :label="L('School')" prop="school">
                  <Input v-model="student.school" />
                </FormItem>
              </i-col>
              <i-col span=6>
                <FormItem :label="L('Grade')" prop="grade">
                  <Select v-model="student.grade" style="z-index:10000" :transfer="true">
                    <Option label="小学一年级" value="小学一年级"></Option>
                    <Option label="小学二年级" value="小学二年级"></Option>
                    <Option label="小学三年级" value="小学三年级"></Option>
                    <Option label="小学四年级" value="小学四年级"></Option>
                    <Option label="小学五年级" value="小学五年级"></Option>
                    <Option label="小学六年级" value="小学六年级"></Option>
                    <Option label="初中一年级" value="初中一年级"></Option>
                    <Option label="初中二年级" value="初中二年级"></Option>
                    <Option label="初中三年级" value="初中三年级"></Option>
                  </Select>
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('SchoolBegin')" prop="schoolBegin">
                  <DatePicker type="date" placeholder="Select date" v-model="student.schooBegin" style="width:100%" :transfer="true"></DatePicker>
                </FormItem>
              </i-col>
            </Row>

          </TabPane>
          <TabPane :label="L('ScoreRank')" name="score">
            <Row :gutter='16'>
              <i-col span=8>
                <FormItem :label="L('ClassRank')" prop="classRank">
                  <Input v-model="student.classRank" />
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('gradeRank')" prop="gradeRank">
                  <Input v-model="student.gradeRank" />
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('Examination')" prop="examination">
                  <Input v-model="student.examination" />
                </FormItem>
              </i-col>
            </Row>

            <Row :gutter='16'>
              <i-col span=8>
                <FormItem :label="L('Chinese')" prop="Chinese">
                  <Input v-model="student.chinese" />
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('Math')" prop="math">
                  <Input v-model="student.math" />
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('English')" prop="english">
                  <Input v-model="student.english" />
                </FormItem>
              </i-col>
            </Row>
            <Row :gutter='16'>
              <i-col span=8>
                <FormItem :label="L('Biology')" prop="biology">
                  <Input v-model="student.biology" />
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('History')" prop="history">
                  <Input v-model="student.history" />
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('Geography')" prop="geography">
                  <Input v-model="student.geography" />
                </FormItem>
              </i-col>
            </Row>
            <Row :gutter='16'>
              <i-col span=8>
                <FormItem :label="L('Physics')" prop="physics">
                  <Input v-model="student.physics" />
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('Chemistry')" prop="chemistry">
                  <Input v-model="student.chemistry" />
                </FormItem>
              </i-col>
              <i-col span=8>
                <FormItem :label="L('Political')" prop="political">
                  <Input v-model="student.political" />
                </FormItem>
              </i-col>
            </Row>
          </TabPane>

          <TabPane :label="L('ParentsInfo')" name="parentsInfo">
            <!-- <FormItem :label="L('Parent')" prop="parent">
              <Input v-model="student.parent" />
            </FormItem>
            <FormItem :label="L('Relation')" prop="relation">
              <Input v-model="student.relation" />
            </FormItem> -->

            <Row :gutter="16">
              <i-col span="8">
                <FormItem :label="L('FatherName')" prop="father">
                  <Input v-model="student.father" />
                </FormItem>
              </i-col>
              <i-col span="16">
                <FormItem :label="L('FatherPhone')" prop="fatherPhone">
                  <Input v-model="student.fatherPhone" />
                </FormItem>
              </i-col>
            </Row>

            <Row :gutter="16">
              <i-col span="8">
                <FormItem :label="L('MotherName')" prop="mother">
                  <Input v-model="student.mother" />
                </FormItem>
              </i-col>
              <i-col span="16">
                <FormItem :label="L('MotherPhone')" prop="motherPhone">
                  <Input v-model="student.motherPhone" />
                </FormItem>
              </i-col>
            </Row>
          </TabPane>

          <TabPane :label="L('ElseInfo')" name="else">
            <!-- <FormItem :label="L('CourseType')" prop="courseType">
              <Input v-model="student.courseType" />
            </FormItem>
            <FormItem :label="L('StudentType')" prop="studentType">
              <Input v-model="student.studentType" />
            </FormItem>
            <FormItem :label="L('TeachMethod')" prop="teachMethod">
              <Input v-model="student.teachMethod" />
            </FormItem>
            <FormItem :label="L('Origin')" prop="origin">
              <Input v-model="student.origin" />
            </FormItem> -->
            <FormItem :label="L('Teacher')" prop="teacherId">
              <Select v-model="student.teacherId" filterable>
                <Option v-for="item in teachers" :value="item.id" :key="item.id" :label="item.id+'  ' +item.name">
                  <span>{{item.id}}</span>
                  <span style="margin-left:10px">{{item.name}}</span>
                </Option>
              </Select>
            </FormItem>
            <FormItem :label="L('Note')" prop="note">
              <Input v-model="student.note" type=textarea :rows='10' />
            </FormItem>
          </TabPane>
        </Tabs>
      </Form>
      <div slot="footer">
        <Button @click="cancel">{{L('Cancel')}}</Button>
        <Button @click="save" type="primary">{{L('OK')}}</Button>
      </div>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "@/store/entities/student";
import Places from "../../../assets/json/city.json";
@Component
export default class EditStudent extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  student: Student = new Student();
  provinces = Places;
  get cities() {
    var list = this.provinces.filter(m => m.name === this.student.province);
    if (list.length === 0) return [];
    return list[0].child;
  }
  get districts() {
    var list = this.cities.filter(m => m.name === this.student.city);
    if (list.length === 0) return [];
    return list[0].child;
  }
  get sex() {
    return this.student.sex
      ? "male"
      : this.student.sex === false
      ? "female"
      : "";
  }

  set sex(val) {
    this.student.sex = val === "male" ? true : val === "female" ? false : null;
  }

  get teachers() {
    return this.$store.state.teacher.list;
  }
  save() {
    (this.$refs.studentForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "student/update",
          data: this.student
        });

        (this.$refs.studentForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.studentForm as any).resetFields();
    this.$emit("input", false);
  }
  async visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.student = Util.extend(
        true,
        {},
        this.$store.state.student.editStudent
      );
      await this.$store.dispatch({
        type: "teacher/getAll"
      });
    }
  }
  studentRule = {
    name: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("StudentName")),
        trigger: "blur"
      }
    ],
    teacherId: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Teacher")),
        trigger: "blur"
      }
    ]
  };
}
</script>

