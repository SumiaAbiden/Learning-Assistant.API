const API_URL = 'http://localhost:5002/api/topics'

export async function getTopics() {
    const response = await fetch(API_URL)

    if (!response.ok) {
        throw new Error('Topics could not be loaded')
    }

    return response.json()
}

// Yeni konu ekleme fonksiyonu (POST)
export async function createTopic(newTopic: { title: string; description: string; category: string; priority: number }) {
    const response = await fetch(API_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        // JavaScript objesini JSON formatına çevirip yolluyoruz
        body: JSON.stringify(newTopic),
    })

    if (!response.ok) {
        throw new Error('Konu eklenirken bir hata oluştu')
    }

    return response.json()
}


// Konu güncelleme (PUT)
export async function updateTopic(id: number, updatedTopic: { title: string; description: string; category: string; status: number; priority: number }) {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(updatedTopic),
    })

    if (!response.ok) throw new Error('Konu güncellenirken hata oluştu')
    return response.json()
}
