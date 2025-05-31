import React, { useState, useEffect } from "react";
import axios from "axios";
import "./App.css";

const API_URL = "https://localhost:7294/api/Tasks";

export default function App() {
  const [tasks, setTasks] = useState([]);
  const [form, setForm] = useState({ title: "", description: "", dueDate: "", status: 0 });
  const [isEditing, setIsEditing] = useState(false);
  const [editId, setEditId] = useState(null);
  const [sortBy, setSortBy] = useState("dueDate");
  const [page, setPage] = useState(1);
  const tasksPerPage = 5;

  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    const res = await axios.get(API_URL);
    setTasks(res.data);
  };

  const handleSubmit = async (e) => {
      
    e.preventDefault();
    const utcDate = new Date(form.dueDate).toISOString();
    const payload = { ...form, dueDate: utcDate };
    if (isEditing) {
      await axios.put(`${API_URL}/${editId}`, payload);
    } else {
      await axios.post(API_URL, payload);
    }
    fetchTasks();
    resetForm();
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this task?")) {
      await axios.delete(`${API_URL}/${id}`);
      fetchTasks();
    }
  };

  const handleEdit = (task) => {
    setForm(task);
    setEditId(task.taskId);
    setIsEditing(true);
  };

  const resetForm = () => {
    setForm({ title: "", description: "", dueDate: "", status: 0 });
    setIsEditing(false);
    setEditId(null);
  };

  const sortedTasks = [...tasks].sort((a, b) => {
    if (sortBy === "dueDate") return new Date(a.dueDate) - new Date(b.dueDate);
    if (sortBy === "status") return a.status - b.status;
    return 0;
  });

  const paginatedTasks = sortedTasks.slice((page - 1) * tasksPerPage, page * tasksPerPage);
  const totalPages = Math.ceil(tasks.length / tasksPerPage);

  return (
    <div className="container">
      <h1 className="title">Hy-Vee Task Management</h1>

      <form onSubmit={handleSubmit} className="task-form">
        <input placeholder="Title" value={form.title} onChange={(e) => setForm({ ...form, title: e.target.value })} required />
        <input placeholder="Description" value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} />
        <input type="datetime-local" value={form.dueDate} onChange={(e) => setForm({ ...form, dueDate: e.target.value })} required />
        <select value={form.status} onChange={(e) => setForm({ ...form, status: parseInt(e.target.value) })}>
          <option value={0}>Madbekuu</option>
          <option value={1}>Madta Idini</option>
          <option value={2}>Madi Aythu</option>
        </select>
        <div className="form-buttons">
          <button type="submit">{isEditing ? "Update" : "Add"} Task</button>
          {isEditing && <button onClick={resetForm} type="button" className="cancel-btn">Cancel</button>}
        </div>
      </form>

      <div className="sort-controls">
        <label>Sort by: </label>
        <select onChange={(e) => setSortBy(e.target.value)} value={sortBy}>
          <option value="dueDate">Due Date</option>
          <option value="status">Status</option>
        </select>
      </div>

      <table className="task-table">
        <thead>
          <tr>
            <th>Title</th>
            <th>Description</th>
            <th>Due Date</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {paginatedTasks.map((t) => (
            <tr key={t.taskId}>
              <td>{t.title}</td>
              <td>{t.description}</td>
              <td>{new Date(t.dueDate).toLocaleString()}</td>
              <td>{["Pending", "In Progress", "Completed"][t.status]}</td>
              <td>
                <button onClick={() => handleEdit(t)} className="edit-btn">Edit</button>
                <button onClick={() => handleDelete(t.taskId)} className="delete-btn">Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="pagination">
        {Array.from({ length: totalPages }, (_, i) => (
          <button key={i} onClick={() => setPage(i + 1)} className={page === i + 1 ? "active" : ""}>
            {i + 1}
          </button>
        ))}
      </div>
    </div>
  );
}