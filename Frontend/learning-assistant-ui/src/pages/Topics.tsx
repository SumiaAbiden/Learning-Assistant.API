import { useState } from 'react'
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { getTopics, createTopic, updateTopic } from '../api/topicApi'
import { PlusCircle, CheckCircle, Clock, PlayCircle, Pencil, X } from 'lucide-react'

export interface Topic {
    id: number;
    title: string;
    description: string;
    category: string;
    status: number;
    priority: number;
    createdAt: string;
}

const STATUS_LABELS: Record<number, string> = {
    0: 'Başlanmadı',
    1: 'Devam Ediyor',
    2: 'Tamamlandı',
    3: 'Beklemede',
}

const getStatusIcon = (status: number) => {
    switch (status) {
        case 0: return <Clock size={18} color="gray" />
        case 1: return <PlayCircle size={18} color="blue" />
        case 2: return <CheckCircle size={18} color="green" />
        default: return <Clock size={18} />
    }
}

function Topics() {
    const queryClient = useQueryClient()
    const [showForm, setShowForm] = useState(false)
    const [editingTopic, setEditingTopic] = useState<Topic | null>(null)

    // Yeni konu formu state
    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')
    const [category, setCategory] = useState('')

    // GET
    const { data: topics, isLoading, isError } = useQuery<Topic[]>({
        queryKey: ['topics'],
        queryFn: getTopics
    })

    // POST
    const createMutation = useMutation({
        mutationFn: createTopic,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['topics'] })
            setTitle(''); setDescription(''); setCategory('')
            setShowForm(false)
        }
    })

    // PUT
    const updateMutation = useMutation({
        mutationFn: ({ id, data }: { id: number; data: any }) => updateTopic(id, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['topics'] })
            setEditingTopic(null)
        }
    })

    const handleCreate = (e: React.FormEvent) => {
        e.preventDefault()
        createMutation.mutate({ title, description, category, priority: 1 })
    }

    const handleUpdate = (e: React.FormEvent) => {
        e.preventDefault()
        if (!editingTopic) return
        updateMutation.mutate({
            id: editingTopic.id,
            data: {
                title: editingTopic.title,
                description: editingTopic.description,
                category: editingTopic.category,
                status: editingTopic.status,
                priority: editingTopic.priority,
            }
        })
    }

    if (isLoading) return <div style={{ padding: '20px' }}>Yükleniyor...</div>
    if (isError) return <div style={{ padding: '20px', color: 'red' }}>Hata oluştu!</div>

    return (
        <div style={{ padding: '20px' }}>
            {/* Başlık + Yeni Ekle Butonu */}
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
                <h1>Öğrenme Konuları</h1>
                <button onClick={() => setShowForm(!showForm)} style={{
                    display: 'flex', alignItems: 'center', gap: '8px',
                    padding: '10px 16px', backgroundColor: showForm ? '#dc3545' : '#646cff',
                    color: 'white', border: 'none', borderRadius: '8px', cursor: 'pointer'
                }}>
                    <PlusCircle size={20} /> {showForm ? 'İptal Et' : 'Yeni Konu Ekle'}
                </button>
            </div>

            {/* Yeni Konu Formu */}
            {showForm && (
                <form onSubmit={handleCreate} style={{
                    marginBottom: '20px', padding: '20px',
                    border: '1px solid #ddd', borderRadius: '8px', backgroundColor: '#f8f9fa'
                }}>
                    <div style={{ display: 'flex', gap: '10px', marginBottom: '10px' }}>
                        <input required placeholder="Konu Başlığı" value={title} onChange={e => setTitle(e.target.value)}
                            style={{ padding: '8px', borderRadius: '4px', border: '1px solid #ccc', flex: 1 }} />
                        <input required placeholder="Kategori" value={category} onChange={e => setCategory(e.target.value)}
                            style={{ padding: '8px', borderRadius: '4px', border: '1px solid #ccc', flex: 1 }} />
                    </div>
                    <textarea placeholder="Açıklama" value={description} onChange={e => setDescription(e.target.value)}
                        style={{ width: '100%', padding: '8px', borderRadius: '4px', border: '1px solid #ccc', marginBottom: '10px', minHeight: '60px' }} />
                    <button type="submit" disabled={createMutation.isPending}
                        style={{ padding: '10px 16px', backgroundColor: '#28a745', color: 'white', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>
                        {createMutation.isPending ? 'Ekleniyor...' : 'Kaydet'}
                    </button>
                </form>
            )}

            {/* Kart Listesi */}
            <div style={{ display: 'grid', gap: '16px', gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))' }}>
                {topics?.map((topic) => (
                    <div key={topic.id} style={{
                        border: '1px solid #ddd', padding: '16px', borderRadius: '8px',
                        backgroundColor: 'white', boxShadow: '0 2px 4px rgba(0,0,0,0.05)'
                    }}>
                        {/* DÜZENLEME FORMU (sadece bu kart için) */}
                        {editingTopic?.id === topic.id ? (
                            <form onSubmit={handleUpdate}>
                                <input value={editingTopic.title}
                                    onChange={e => setEditingTopic({ ...editingTopic, title: e.target.value })}
                                    style={{ width: '100%', padding: '6px', marginBottom: '8px', borderRadius: '4px', border: '1px solid #ccc' }} />
                                <input value={editingTopic.category}
                                    onChange={e => setEditingTopic({ ...editingTopic, category: e.target.value })}
                                    style={{ width: '100%', padding: '6px', marginBottom: '8px', borderRadius: '4px', border: '1px solid #ccc' }} />
                                <textarea value={editingTopic.description}
                                    onChange={e => setEditingTopic({ ...editingTopic, description: e.target.value })}
                                    style={{ width: '100%', padding: '6px', marginBottom: '8px', borderRadius: '4px', border: '1px solid #ccc' }} />

                                {/* Durum Seçimi */}
                                <select value={editingTopic.status}
                                    onChange={e => setEditingTopic({ ...editingTopic, status: Number(e.target.value) })}
                                    style={{ width: '100%', padding: '6px', marginBottom: '8px', borderRadius: '4px', border: '1px solid #ccc' }}>
                                    {Object.entries(STATUS_LABELS).map(([val, label]) => (
                                        <option key={val} value={val}>{label}</option>
                                    ))}
                                </select>

                                <div style={{ display: 'flex', gap: '8px' }}>
                                    <button type="submit" disabled={updateMutation.isPending}
                                        style={{ padding: '6px 12px', backgroundColor: '#28a745', color: 'white', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>
                                        {updateMutation.isPending ? 'Kaydediliyor...' : 'Kaydet'}
                                    </button>
                                    <button type="button" onClick={() => setEditingTopic(null)}
                                        style={{ padding: '6px 12px', backgroundColor: '#6c757d', color: 'white', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>
                                        İptal
                                    </button>
                                </div>
                            </form>
                        ) : (
                            /* NORMAL GÖRÜNÜM */
                            <>
                                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
                                    <h3 style={{ margin: '0 0 8px 0', color: '#333' }}>{topic.title}</h3>
                                    <div style={{ display: 'flex', gap: '8px', alignItems: 'center' }}>
                                        {getStatusIcon(topic.status)}
                                        <button onClick={() => setEditingTopic(topic)}
                                            style={{ background: 'none', border: 'none', cursor: 'pointer', color: '#888', padding: '2px' }}>
                                            <Pencil size={16} />
                                        </button>
                                    </div>
                                </div>
                                <p style={{ margin: '0 0 12px 0', fontSize: '14px', color: '#666' }}>
                                    {topic.description || "Açıklama yok"}
                                </p>
                                <div style={{ display: 'flex', gap: '8px', flexWrap: 'wrap' }}>
                                    <span style={{ fontSize: '12px', padding: '4px 8px', backgroundColor: '#f0f0f0', borderRadius: '4px', color: '#555' }}>
                                        {topic.category}
                                    </span>
                                    <span style={{ fontSize: '12px', padding: '4px 8px', backgroundColor: '#e8f4f8', borderRadius: '4px', color: '#0066cc' }}>
                                        {STATUS_LABELS[topic.status]}
                                    </span>
                                </div>
                            </>
                        )}
                    </div>
                ))}
            </div>
        </div>
    )
}

export default Topics
