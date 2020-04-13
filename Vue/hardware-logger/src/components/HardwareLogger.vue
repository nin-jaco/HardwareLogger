<template>
  <div class="container-fluid mt-4">
    <h1 class="h1">Hardware Logger</h1>
    <b-alert :show="loading" variant="info">Loading...</b-alert>
    <b-row>
      <b-col>
        <table class="table table-striped">
          <thead>
            <tr>
              <th>Id</th>
              <th>Type</th>
              <th>Description</th>
              <th>Serial Number</th>
              <th>Photo</th>
			  <th>Purchase Price</th>
              <th>&nbsp;</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="record in records" :key="record.Id">
              <td>{{ record.Id }}</td>
              <td>{{ record.Type }}</td>
              <td>{{ record.Description }}</td>
              <td>{{ record.SerialNumber }}</td>
			  <td><img :src="record.Photo" alt="Photo" /></td>
			  <td>{{ record.PurchasePrice }}</td>
              <td class="text-right">
                <a href="#" @click.prevent="updateRecord(record)">Edit</a> -
                <a href="#" @click.prevent="deleteRecord(record.Id)">Delete</a>
              </td>
            </tr>
          </tbody>
        </table>
      </b-col>
      <b-col lg="3">
        <b-card :title="(model.Id ? 'Edit Hardware Id#' + model.Id : 'New Hardware Record')">
          <form @submit.prevent="createRecord">
            <b-form-group label="Type">
              <b-form-input type="text" v-model="model.Type"></b-form-input>
            </b-form-group>
            <b-form-group label="Description">
              <b-form-input rows="4" v-model="model.Description" type="text"></b-form-input>
            </b-form-group>
            <b-form-group label="SerialNumber">
              <b-form-input rows="4" v-model="model.SerialNumber" type="text"></b-form-input>
            </b-form-group>
			<b-form-group label="Photo">
              <b-form-input rows="4" v-model="model.Photo" type="text"></b-form-input>
            </b-form-group>
			<b-form-group label="PurchasePrice">
              <b-form-input rows="4" v-model="model.PurchasePrice" type="number" min="0.01" step="0.01" max="2500"></b-form-input>
			</b-form-group>
            <div>
              <b-btn type="submit" variant="success">Save Record</b-btn>
            </div>
          </form>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
  import api from '@/HardwareLoggerApiService';

  export default {
    data() {
      return {
        loading: false,
        records: [],
        model: {}
      };
    },
    async created() {
      this.getAll()
    },
    methods: {
      async getAll() {
        this.loading = true

        try {
          this.records = await api.getAll()
        } finally {
          this.loading = false
        }
      },
      async updateRecord(record) {
        // We use Object.assign() to create a new (separate) instance
        this.model = Object.assign({}, record)
      },
      async createRecord() {
        const isUpdate = !!this.model.Id;

        if (isUpdate) {
          await api.update(this.model.Id, this.model)
        } else {
          await api.create(this.model)
        }

        // Clear the data inside of the form
        this.model = {}

        // Fetch all records again to have latest data
        await this.getAll()
      },
      async deleteRecord(Id) {
        if (confirm('Are you sure you want to delete this record?')) {
          // if we are editing a food record we deleted, remove it from the form
          if (this.model.id === Id) {
            this.model = {}
          }

          await api.delete(Id)
          await this.getAll()
        }
      }
    }
  }
</script>